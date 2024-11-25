from selenium import webdriver
from selenium.webdriver.chrome.service import Service
from webdriver_manager.chrome import ChromeDriverManager
from bs4 import BeautifulSoup
import time
import pandas as pd

data = []

driver = webdriver.Chrome(service=Service(ChromeDriverManager().install()))

for page in range(1, 128):
    url = f'https://visitjeju.net/kr/detail/list?menuId=DOM_000001718000000000&cate1cd=cate0000000002#p{page}&pageSize=12&sortListType=reviewcnt&viewType=map&isShowBtag&tag'
    driver.get(url)

    time.sleep(5)

    soup = BeautifulSoup(driver.page_source, 'html.parser')

    attractions = soup.select('li dl.item_section_new')

    for attraction in attractions:
        name = attraction.select_one('p.s_tit').get_text(strip=True)
        
        link = 'https://visitjeju.net' + attraction.select_one('a')['href']
        
        image_url = attraction.select_one('img')['src']
        
        data.append({
            "관광지 이름": name,
            "이미지 URL": image_url,
            "링크": link
        })

driver.quit()

df = pd.DataFrame(data)

print(df)
df.to_csv("wwwroot/db/attraction_data.csv", index=False, encoding='utf-8-sig')
print("데이터가 CSV 파일로 저장되었습니다.")
