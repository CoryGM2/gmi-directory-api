--  Adds support for creating UUIDs if it doesn't already exist
CREATE EXTENSION IF NOT EXISTS 'uuid-ossp';

--  Creates based on the MAC address of the server
SELECT uuid_generate_v1();

--  Creates based on completely random (as far as computers can do random) numbers
SELECT uuid_generate_v4();

drop table dbo.Person;

create table dbo.Person (
		Id					int				not null			primary key,
		FirstName			varchar(255)	not null,
		LastName			varchar(255)	not null
);

create index idx_PersonLastName on dbo.Person (LastName);
create index idx_PersonFirstName on dbo.Person (FirstName);

commit;
	
insert into dbo.Person (Id, FirstName, LastName) values (1, 'John', 'Smith');
insert into dbo.Person (Id, FirstName, LastName) values (2, 'Joan', 'Riker');
insert into dbo.Person (Id, FirstName, LastName) values (3, 'Nancy', 'Drew');
insert into dbo.Person (Id, FirstName, LastName) values (4, 'Scarlet', 'Meyer');
insert into dbo.Person (Id, FirstName, LastName) values (5, 'Richard', 'Scott');
insert into dbo.Person (Id, FirstName, LastName) values (6, 'Caleb', 'McDougal');
insert into dbo.Person (Id, FirstName, LastName) values (7, 'Genevieve', 'Smith');
insert into dbo.Person (Id, FirstName, LastName) values (8, 'Mickey', 'Rutherford');
insert into dbo.Person (Id, FirstName, LastName) values (9, 'Jackson', 'Hayes');
insert into dbo.Person (Id, FirstName, LastName) values (10, 'Jenny', 'Newman');
insert into dbo.Person (Id, FirstName, LastName) values (11, 'Sarah', 'Bannister');

commit;

select *
	from dbo.person