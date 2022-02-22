use project1
go

Create table Destination(
	DestinationID int,
	DestinationName varchar(200)
)

Create table Flight(
	FlightID int,
	Date DateTime,
	Price Decimal(18,2),
	FromID int,
	ToID int
)





create table Payment(
PaymentID int Identity(1,1) Primary Key,
TotalAmount decimal(18,2),
CardNumber nvarchar(200),
CardHolder nvarchar(200),
CVV nvarchar(6),
Expiration nvarchar(10)
)


create table [Order](
OrderID int Identity(1,1) Primary Key,
TrackingID uniqueidentifier,
DepartFlightID int NULL,
ReturnFlightID int NULL,
PaymentID int,
Constraint FK_OrderPayment Foreign Key (PaymentID)
References Payment(PaymentID)
)