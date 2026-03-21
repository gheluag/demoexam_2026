
drop database if exists ShoeShop;
create database if not exists ShoeShop;
use ShoeShop;


create table if not exists UsersRoles(
idRole int primary key auto_increment,
RoleName varchar(255) not null 
);

create table if not exists Users(
idUser int primary key auto_increment,
LastName varchar(255) not null, 
FirstName varchar(255) not null, 
MiddleName varchar(255) not null, 
Email varchar(255) not null unique, 
Passw varchar(255) not null, 
RoleId int not null,
foreign key(RoleId) references UsersRoles(idRole) on delete cascade on update cascade
);

create table if not exists Categories(
idCat int primary key auto_increment,
CatName varchar(255) not null 
);

create table if not exists Manufacrurers(
idManuf int primary key auto_increment,
ManufName varchar(255) not null 
);

create table if not exists Suppliers(
idSup int primary key auto_increment,
SupName varchar(255) not null 
);

create table if not exists ProdNames(
idProdName int primary key auto_increment,
ProdName varchar(255) not null 
);

drop table if exists Products;
create table if not exists Products(
idProd int primary key auto_increment,
Article varchar(255) not null, 
ProdName int not null, 
Price decimal(10, 2) not null, 
Sale int not null, 
Count int not null, 
Descrip text, 
Image text, 
SupId int not null,
ManufId int not null,
CatId int not null,
foreign key(SupId) references Suppliers(idSup) on delete cascade on update cascade,
foreign key(ProdName) references ProdNames(idProdName) on delete cascade on update cascade,
foreign key(ManufId) references Manufacrurers(idManuf) on delete cascade on update cascade,
foreign key(CatId) references Categories(idCat) on delete cascade on update cascade
);

create table if not exists Pvz(
idPvz int primary key auto_increment,
Adress varchar(255) not null 
);

create table if not exists OrderStatus(
idStatus int primary key auto_increment,
StatusName varchar(255) not null 
);

create table if not exists Orders(
idOrder int primary key auto_increment,
DateOrder date not null,
DateDelivery date,
Codes int,
PvzId int,
StatusId int,
foreign key(PvzId) references Pvz(idPvz) on delete cascade on update cascade,
foreign key(StatusId) references OrderStatus(idStatus) on delete cascade on update cascade
);


create table if not exists ProdOrders(
idProdOrder int primary key auto_increment,
ProdId int,
OrderId int,
Count int,
foreign key(ProdId) references Products(idProd) on delete cascade on update cascade,
foreign key(OrderId) references Orders(idOrder) on delete cascade on update cascade
);

create table if not exists UsersOrders(
idUserOrder int primary key auto_increment,
UserId int,
OrderId int,
foreign key(UserId) references Users(idUser) on delete cascade on update cascade,
foreign key(OrderId) references Orders(idOrder) on delete cascade on update cascade
);





