create table crm
(
id int identity(1,1) primary key
,servicetype varchar(50)
,clientname varchar(100)
,city  varchar(50)
,state varchar(50)
,country varchar(50)
,subscriberbase varchar(20)
,otherinfo varchar(100)
,approxdealvalue varchar(50)
,dealstatuswon bit
,dealstatuslost bit
,lostreason varchar(200)
,lostremarks  varchar(200)
,active bit
)

create table crm_contact
(
id int identity(1,1)
,crmid int references crm
,name varchar(100)
,contact varchar(100)
,mailid varchar(100)
,active bit
)

create table crm_statusupdate
(
id int identity(1,1)
,crmid int references crm
,statusdate datetime
,remarks(200)
,followup bit
,followupdate datetime
,active bit
)

create table crm_deal
(
 id int identity(1,1)
,crmid int references crm
,gst varchar(20)
,pan varchar(20)
,concernedperson varchar(100)
,designation varchar(100)
,addressline varchar(200)
,pricing varchar(20)
,tenure varchar(10)
,startdate datetime
,enddate datetime
,exclusivetaxes bit
,channelsubscribed int
,languages varchar(50)
,otherfeatures varchar(100)
,proglengthandsynopsis varchar(10)
,systemname varchar(50)
,fileformat varchar(20)
,paying bit
,active bit
)