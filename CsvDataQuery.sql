--select count(*) from CvsData;

--select * from CvsData

--select PUlocationId, avg(tip_amount) as avg_tip_amount
--from CvsData
--group by PULocationID
--order by avg_tip_amount desc

--select top 100 * from CvsData
--order by trip_distance desc

--select top 100 * from CvsData
--order by datediff(minute, tpep_pickup_datetime, tpep_dropoff_datetime) desc