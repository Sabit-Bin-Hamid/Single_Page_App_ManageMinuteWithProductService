-- masterminutes
---------------------
--------------------
create or alter proc dbo.Meeting_Minutes_Master_Save_SP
    @CustomerType nvarchar(50),
    @CustomerId int,
    @MeetingDate date,
    @MeetingTime time(0),
    @MeetingPlace nvarchar(200),
    @AttendsFromClientSide nvarchar(500),
    @AttendsFromHostSide nvarchar(500),
    @MeetingAgenda nvarchar(500),
    @MeetingDiscussion nvarchar(2000),
    @MeetingDecision nvarchar(1000),
    @NewId int output
    as
    begin
        insert into dbo.Meeting_Minutes_Master_Tbl
            (
                CustomerType,CustomerId,MeetingDate,MeetingTime,MeetingPlace, AttendsFromClientSide,
                AttendsFromHostSide, MeetingAgenda,MeetingDiscussion, MeetingDecision
            )
        values
            (
                @CustomerType,@CustomerId, @MeetingDate,@MeetingTime,@MeetingPlace,@AttendsFromClientSide,
                @AttendsFromHostSide, @MeetingAgenda,@MeetingDiscussion,@MeetingDecision
            );
        set @NewId = cast(SCOPE_IDENTITY() as int);
    end
    go


--details insert
----------------
---------------
create or alter proc Meeting_Minutes_Details_Save_SP
    @Details MeetingMinuteDetailType readonly
as
begin
    insert into Meeting_Minutes_Details_Tbl
    (
        MeetingMinuteId,
        ProductServiceId,
        Quantity,
        Unit
    )
    select 
        MeetingMinuteId,ProductServiceId,Quantity,Unit
    from @Details;
end
Go























































































---------------------------
--------------------------
--details
create or alter proc dbo.Meeting_Minutes_Details_Save_SP_old
    @MeetingMinuteId   int,
    @ProductServiceId  int,
    @Quantity decimal(18,2),
    @Unit nvarchar(50)
    as
    begin
        set nocount on;

        insert into dbo.Meeting_Minutes_Details_Tbl (MeetingMinuteId,ProductServiceId, Quantity, Unit )
        values (@MeetingMinuteId, @ProductServiceId,@Quantity, @Unit);
    end
 GO
 --------------
 -------------------





































































 ----------------------
 --------------------
 Create OR Alter Proc Get_All_Meeting_Minutes_SP
As
Begin

    Select 
        mm.MeetingMinuteId,
        mm.CustomerType,
        mm.CustomerId,
        mm.MeetingDate,
        mm.MeetingTime,
        mm.MeetingPlace,
        mm.AttendsFromClientSide,
        mm.AttendsFromHostSide,
        mm.MeetingAgenda,
        mm.MeetingDiscussion,
        mm.MeetingDecision
    from Meeting_Minutes_Master_Tbl mm
end
go
---------------------------------
---------------------------------
create or alter proc Get_All_Product_BasedONMeetigMinute_SP
	@MeetingMinuteId INT
as
begin
	select 
		a.MeetingMinuteDetailId,
		a.MeetingMinuteId,
		a.ProductServiceId,
		a.Quantity,
		a.Unit ,
		b.Name
	from Meeting_Minutes_Details_Tbl a join Products_Service_Tbl b
	on a.ProductServiceId=b.ProductServiceId
	where MeetingMinuteId=@MeetingMinuteId
end
go