import requests
from bs4 import BeautifulSoup
import csv
import re

# 기본 URL 설정
base_url = "https://jobs.chu.ac.kr/board/job_bbs.php?bo_table=job&ds=IGFuZCAoIHdyX3JlZ2lvbiBsaWtlICclaDAlJyAgKSA="

url = base_url.format(1)
response = requests.get(url)
soup = BeautifulSoup(response.text, 'html.parser')

all_notices = []

#마지막 페이지 추출
first_page_url = base_url.format(1)
response = requests.get(first_page_url)
soup = BeautifulSoup(response.text, 'html.parser')

last_page_link = soup.find('a', class_='btn-last')
last_page_attr = last_page_link['href']
page_index = int(re.search(r'\d+', last_page_attr).group())

for page in range(1, page_index + 1):
    url = base_url.format(page)
    response = requests.get(url)
    soup = BeautifulSoup(response.text, 'html.parser')

    for row in soup.select("tbody tr"):
        name = row.select_one('td.left').get_text(strip=True)
    
        title = row.select_one('td.left a').get_text(strip=True)
    
        link = row.select_one('td.left a')['href']
        full_link = "https://jobs.chu.ac.kr/" + link

        date = row.select('td')
        if len(date) >= 5:
            reg_date = date[3].get_text()
            reg_date = re.sub(r'\s+', '', reg_date)
        close_date = row.select_one('strong').get_text(strip=True)

        all_notices.append({
            '회사명': name,
            '제목': title,
            '작성일': reg_date,
            '마감일': close_date,
            '주소': full_link
            })
    
csv_file_path = r'wwwroot/db/halla.csv'
with open(csv_file_path, mode='w', newline='', encoding='utf-8-sig') as csv_file:
    fieldnames = ['회사명', '제목', '작성일', '마감일', '주소']
    writer = csv.DictWriter(csv_file, fieldnames=fieldnames)
    writer.writeheader()

    for notice in all_notices:
        writer.writerow(notice)

print(f'데이터가 {csv_file_path}에 저장되었습니다.')