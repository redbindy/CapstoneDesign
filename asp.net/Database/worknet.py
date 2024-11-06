import requests
from bs4 import BeautifulSoup
import csv
import re

# 기본 URL 설정
base_url = "https://www.work.go.kr/empInfo/empInfoSrch/list/dtlEmpSrchList.do?resultCnt=50&codeDepth2Info=11000&sortFieldInfo=DATE&sortField=DATE&sortOrderBy=DESC&benefitSrchAndOr=O&webIsOut=region&keywordStaAreaNm=N&listCookieInfo=DTL&codeDepth1Info=11000&empTpGbcd=1&region=50000&resultCntInfo=50&siteClcd=all&sortOrderByInfo=DESC&currntPageNo=1&keywordWantedTitle=N&essCertChk=N&isEmptyHeader=Y&depth2SelCode=&_csrf=f5a1dfd8-53f2-43b1-9d10-161c643cbaa9&keywordBusiNm=N&staAreaLineInfo1=11000&staAreaLineInfo2=1&pageIndex={}"

all_notices = []

#마지막 페이지 추출
first_page_url = base_url.format(1)
response = requests.get(first_page_url)

soup = BeautifulSoup(response.text, 'html.parser')

last_page_link = soup.find('a', class_='control last')
onclick_attr = last_page_link.get('onclick')
page_index = int(re.search(r'\d+', onclick_attr).group())

num = 0
#크롤링 시작
for page in range(1, page_index + 1):
    url = base_url.format(page)
    response = requests.get(url)
    soup = BeautifulSoup(response.text, 'html.parser')

    for row in soup.select("table.board-list tbody tr"):

        name_tag = row.select_one("a.cp_name") or row.select_one("td.a-l.va-t") #회사명
        name = name_tag.get_text(strip=True, separator='\n').splitlines()[0]

        title = row.select_one("div.cp-info-in a").get_text(strip=True) #제목

        link = row.select_one("div.cp-info-in a")['href'] #주소
        full_link = 'https://www.work.go.kr' + link

        pay_tag = row.select_one("td.a-l.va-t div.cp-info strong.font-black") or row.select_one("td.a-l.va-t div.cp-info strong") #급여
        pay = pay_tag.find_parent().get_text(strip=True)
        pay = re.sub(r'\s+', '', pay)

        date_tag = row.select_one("td.va-t div.cp-info p.dday.mt10") #등록일, 작성일
        date_text = date_tag.find_parent().get_text(strip=True)
        reg_date = date_text.split("등록")[0].strip()
        close_date = date_text.split("마감")[0].split("등록")[-1].strip() if date_text.split("마감")[0].split("등록")[-1].strip() else "채용시까지"

        all_notices.append({
                    '회사명': name,
                    '제목': title,
                    '마감일': close_date,
                    '주소': full_link,
                    '급여': pay
                })

csv_file_path = './worknet.csv'
with open(csv_file_path, mode='w', newline='', encoding='utf-8-sig') as csv_file:
    fieldnames = ['회사명', '제목', '마감일', '주소', '급여']
    writer = csv.DictWriter(csv_file, fieldnames=fieldnames)
    writer.writeheader()

    for notice in all_notices:
        writer.writerow(notice)

print(f'데이터가 {csv_file_path}에 저장되었습니다.')
