from selenium import webdriver
from selenium.webdriver.chrome.service import Service
from webdriver_manager.chrome import ChromeDriverManager
from selenium.webdriver.common.by import By
from selenium.webdriver.support.ui import WebDriverWait
from selenium.webdriver.support import expected_conditions as EC
from selenium.webdriver.common.keys import Keys
import time
import pandas as pd

# 크롬 드라이버 설치 및 실행
driver = webdriver.Chrome(service=Service(ChromeDriverManager().install()))

# 복지로 사이트로 접속
url = 'https://www.bokjiro.go.kr/ssis-tbu/twataa/wlfareInfo/moveTWAT52005M.do'
driver.get(url)

# 페이지 로드 대기
time.sleep(5)

# '청년' 버튼 클릭
youth_button = driver.find_element(By.CSS_SELECTOR, 'div[data-value="청년"] div.cl-checkbox-icon')
youth_button.click()

# '제주특별자치도'를 찾기 위해 아래 방향키를 반복적으로 누르기
while True:
    region_combobox = WebDriverWait(driver, 10).until(
        EC.element_to_be_clickable((By.CSS_SELECTOR, 'div[role="combobox"]'))
    )
    current_selection = region_combobox.get_attribute("aria-label")

    # 제주특별자치도가 선택되면 Enter 키를 눌러 선택
    if "제주특별자치도" in current_selection:
        region_combobox.send_keys(Keys.ENTER)
        break
    else:
        region_combobox.send_keys(Keys.ARROW_DOWN)
        time.sleep(0.5)  # 아래 방향키를 누르고 잠시 대기

# 검색 버튼 클릭
search_button = WebDriverWait(driver, 10).until(
    EC.element_to_be_clickable((By.XPATH, '//a[@title="검색 버튼"]'))
)
search_button.click()

# 결과 페이지가 로드될 시간을 대기
time.sleep(5)

# 복지 서비스 데이터를 담을 빈 리스트 생성
welfare_data = {
    '복지서비스 제목': [],
    '복지서비스 내용': [],
    '담당부처': [],
    '지원주기': [],
    '제공유형': [],
    '문의처': []
}

# 데이터 크롤링 함수 정의
def extract_service_data():
    service_ids = ['service-1', 'service-2']  # id 속성값 리스트
    for service_id in service_ids:
        try:
            # 각 항목의 id를 사용하여 복지 서비스 제목을 추출
            service_name = driver.find_element(By.CSS_SELECTOR, "[data-ndid=\"ks\"]").text
            
            str = service_name.split("\n")
            for i in range(len(str)):
                s = str[i]
                if s == "담당부처":
                    welfare_data["복지서비스 제목"].append(str[i - 2])
                    welfare_data["복지서비스 내용"].append(str[i - 1])
                    welfare_data['담당부처'].append(str[i + 1])
                    welfare_data["지원주기"].append(str[i + 3])
                    welfare_data["제공유형"].append(str[i + 5])
                    welfare_data["문의처"].append(str[i + 7])
        except Exception as e:
            print(f"Error while scraping service with id {service_id}: {e}")

# 페이지 수 만큼 반복하며 데이터 추출
for page in range(1, 6):  # 1부터 5페이지까지
    print(f"{page}페이지 데이터 추출 중...")
    extract_service_data()

    # 다음 페이지로 이동 (마지막 페이지 제외)
    if page < 5:
        try:
            next_page_button = WebDriverWait(driver, 10).until(
                EC.element_to_be_clickable((By.CSS_SELECTOR, f'div[role="button"][data-index="{page + 1}"]'))
            )
            next_page_button.click()
            time.sleep(5)  # 페이지 로딩 대기
        except Exception as e:
            print(f"{page + 1}페이지로 이동 중 오류가 발생했습니다:", e)
            break

# 데이터프레임 생성
df = pd.DataFrame(welfare_data)

# 데이터가 비어있는지 확인
if df.empty:
    print("크롤링한 데이터가 비어 있습니다.")
else:
    # 데이터프레임을 CSV 파일로 저장
    df.to_csv('welfare_service.csv', index=False, encoding = "utf-8")
    print("크롤링 완료 및 CSV 파일 저장 완료")

# 브라우저 닫기
driver.quit()
