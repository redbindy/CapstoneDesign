from selenium import webdriver
from selenium.webdriver.chrome.service import Service
from webdriver_manager.chrome import ChromeDriverManager
from bs4 import BeautifulSoup
import time
import pandas as pd

data = []

driver = webdriver.Chrome(service=Service(ChromeDriverManager().install()))

for page in range(1, 7):
    url = f'https://jejuyouthdream.com/policy?category=CT_EDUCATION&page={page}'
    driver.get(url)

    time.sleep(5)

    soup = BeautifulSoup(driver.page_source, 'html.parser')

    section = soup.select_one('section.max-xl.container')  
    

    titles = section.select('strong.font-bold')  
    institutions = section.select('p.break-keep') 
    ddays = section.select('i.font-Montserrat')  

    for title, institution, dday in zip(titles, institutions, ddays):
        data.append({
            "제목": title.get_text(strip=True),
            "담당 기관": institution.get_text(strip=True),
            "디데이": dday.get_text(strip=True)
        })

driver.quit()

df = pd.DataFrame(data)

df.to_csv("edudata.csv", index=False, encoding='utf-8-sig') 
print("데이터가 CSV 파일로 저장되었습니다.")