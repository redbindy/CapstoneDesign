import pandas as pd
import matplotlib.pyplot as plt
import sqlite3
import plotly.express as px

connection = sqlite3.connect("wwwroot/db/WebDB.db")

cursor = connection.cursor()

query = "select datetime from VisitLog"
cursor.execute(query)

data = cursor.fetchall()

df = pd.DataFrame(data, columns = ["Datetime"])

df["Datetime"] = pd.to_datetime(df["Datetime"])
df["Year"] = df["Datetime"].dt.year
df["Month"] = df["Datetime"].dt.month
df["Day"] = df["Datetime"].dt.day

fig = px.bar(df["Day"])
fig.update_xaxes(title_text = "일")
fig.update_yaxes(title_text = "방문자수")

fig.write_html("wwwroot/db/VisitorGraph.html")

connection.close()