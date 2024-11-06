import pandas as pd
import sqlite3

welfare = pd.read_csv("./welfare_service.csv")

conn = sqlite3.connect("../wwwroot/db/WebDB.db")
c = conn.cursor()

count = welfare.shape[0]
formatStr = "insert into Welfare (Title, Content, Ministry, Period, ServiceType, Contect) values(\'{0}\', \'{1}\', \'{2}\', \'{3}\', \'{4}\', \'{5}\')";

for i in range(count):
    row = welfare.iloc[i, :]
    query = formatStr.format(row[0], row[1], row[2], row[3], row[4], row[5])
    print(query)
    c.execute(query)
    conn.commit()

conn.close()