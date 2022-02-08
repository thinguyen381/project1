use project1
go
Create table Flight(
	FlightID int,
	Date DateTime,
	Price Decimal(18,2),
	FromID int,
	ToID int
)

Create table Invoice(
	InvoiceID int,
	CustomerID int
)

Create table Line(
	LineID int,
	OrderID int,
	ScheduleID int
)

Create table Destination(
	DestinationID int,
	DestinationName varchar(200)
)


--Create table Class(
--	ClassID int,
--	ClassName varchar(50),
--	[Service] varchar(200)
--)

--Create table Customer(
--	CustomerID int,
--	Name varchar(100),
--	Email varchar(100),
--	CreditCardNumber varchar(16),

--)

