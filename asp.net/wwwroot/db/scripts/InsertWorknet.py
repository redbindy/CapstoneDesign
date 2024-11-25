import pandas as pd
import sqlite3

worknet = pd.read_csv("wwwroot/db/worknet.csv")

conn = sqlite3.connect("wwwroot/db/WebDB.db")
c = conn.cursor()

count = worknet.shape[0]
formatStr = "insert into Job (Recruitor, Title, DueDate, Url, Pay) values(\'{0}\', \'{1}\', \'{2}\', \'{3}\', \'{4}\')";

for i in range(count):
    row = worknet.iloc[i, :]
    query = formatStr.format(row[0], row[1].replace('\'', "-"), row[2], row[3], row[4])
    print(query)
    c.execute(query)
    conn.commit()

conn.close()