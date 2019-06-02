create table Estoque(
	IdEstoque integer	identity(1,1),
	Nome	nvarchar(150) not null unique,
	primary key (IdEstoque))


create table Produto(
	IdProduto integer identity(1,1),
	Nome	nvarchar(150) not null ,
	Preco	decimal not null ,
	Quantidade integer not null,
	IdEstoque integer not null,
	primary key(IdProduto),
	foreign key(IdEstoque) references Estoque (IdEstoque))

