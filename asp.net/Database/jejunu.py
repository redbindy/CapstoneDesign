from ast import increment_lineno
import requests
from bs4 import BeautifulSoup
import csv
import re

# 기본 URL
base_url = 'https://clip.jejunu.ac.kr/_bbs/basic_user_notice/list.asp?mcd=ms060201&slistnum=50&sopttype=subject&page={}'

# 모든 페이지의 공지 정보를 저장할 리스트
all_notices = []

# 첫 페이지를 크롤링하여 전체 페이지 수를 확인
first_page_url = base_url.format(1)
response = requests.get(first_page_url)

if response.status_code == 200:
    soup = BeautifulSoup(response.text, 'html.parser')
    
    # 마지막 페이지 링크 찾기
    last_page_link = soup.find('a', title='마지막 페이지로 이동')
    
    if last_page_link and 'href' in last_page_link.attrs:
        # href에서 페이지 번호 추출
        last_page_href = last_page_link['href']
        # 페이지 번호 추출
        match = re.findall(r'page=(\d+)', last_page_href)
        if match:
            page_number = int(match[-1])  # 마지막 페이지 숫자 추출
    else:
        print('마지막 페이지 링크를 찾을 수 없습니다.')
        page_number = 1  # 링크가 없을 경우 1페이지로 설정
else:
    print(f'Error: {response.status_code} on first page')
    page_number = 1  # 에러 발생 시 1페이지로 설정

# 각 페이지에서 데이터를 크롤링
for page in range(1, page_number + 1):
    url = base_url.format(page)
    response = requests.get(url)

    if response.status_code == 200:
        soup = BeautifulSoup(response.text, 'html.parser')
        rows = soup.find_all('tr')

        for row in rows:
            columns = row.find_all('td')

            if len(columns) >= 3:
                number = columns[0].get_text(strip=True)
                title = columns[1].get_text(strip=True)
                date = columns[2].get_text(strip=True)

                title_link = columns[1].find('a')
                if title_link and 'href' in title_link.attrs:
                    link = title_link['href']
                    full_link = 'https://clip.jejunu.ac.kr/_bbs/basic_user_notice/' + link  # 절대 경로로 변환
                else:
                    full_link = '링크 없음'

                all_notices.append({
                    '번호': number,
                    '제목': title,
                    '작성일': date,
                    '주소': full_link
                })
    else:
        print(f'Error: {response.status_code} on page {page}')

# CSV 파일로 저장
csv_file_path = r'C:\Users\82105\Downloads\jejunu.csv'
with open(csv_file_path, mode='w', newline='', encoding='utf-8-sig') as csv_file:
    fieldnames = ['번호', '제목', '작성일', '주소']
    writer = csv.DictWriter(csv_file, fieldnames=fieldnames)
    writer.writeheader()

    for notice in all_notices:
        writer.writerow(notice)

print(f'데이터가 {csv_file_path}에 저장되었습니다.')





