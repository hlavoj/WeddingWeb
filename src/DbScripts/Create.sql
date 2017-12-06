
create table Wedding (
Id int not null Primary key identity ,
Identifikator varchar(10),
Firstname varchar(50),
Secondname varchar(50),
DisplayName varchar(100),
Email varchar(50),
Number varchar(50),
InvitationType int ,
QrCodeOpend int not null,
PhoneNumberCalled int not null,
Questions varchar(20)
)

create table WeddingAttemp(
Id int not null Primary key identity,
IdWedding int not null FOREIGN KEY REFERENCES Wedding(Id),
OpenTime DateTime not null,
Participation int, -- 0-unknown 1-going 2-mabe 3-wont go
Question1 int, -- 0-unknown 1-going 2-mabe 3-wont go
Question2 int, -- 0-unknown 1-going 2-mabe 3-wont go
Question3 int, -- 0-unknown 1-going 2-mabe 3-wont go
Question4 int, -- 0-unknown 1-going 2-mabe 3-wont go
Email varchar(50),
Number varchar(50)
)



insert into Wedding (Identifikator,Firstname, Secondname, DisplayName, Email, Number, InvitationType, QrCodeOpend, PhoneNumberCalled, Questions) Values
('456','Tereza','Loffelmannová','Terezo Loffelmannová','loffik@seznam.cz','789456132','2','0','0','1');

-- drop table Wedding
-- drop table WeddingAttemp