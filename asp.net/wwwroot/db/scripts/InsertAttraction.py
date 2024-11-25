import pandas as pd
import sqlite3

attractions = pd.read_csv("wwwroot/db/attraction_data.csv")

conn = sqlite3.connect("wwwroot/db/WebDB.db")
c = conn.cursor()

count = attractions.shape[0]
formatStr = "insert into Attraction (Name, ImgUrl, Url) values(\'{0}\', \'{1}\', \'{2}\')";

for i in range(count):
    row = attractions.iloc[i, :]
    query = formatStr.format(row[0], row[1], row[2])
    print(query)
    c.execute(query)
    conn.commit()

conn.close()