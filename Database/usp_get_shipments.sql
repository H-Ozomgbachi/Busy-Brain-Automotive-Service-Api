CREATE PROCEDURE [dbo].[usp_get_shipments]
	 @org_id  int,
     @PageNo INT = 1,  
     @PageSize INT = 25, 
     @tracking_number NVARCHAR(50) = NULL,
     @route_id INT = NULL,
     @recipient_name NVARCHAR(50) = NULL
  AS BEGIN  
    SET NOCOUNT ON;
    SET @tracking_number = LTRIM(RTRIM(@tracking_number))  
    SET @recipient_name = LTRIM(RTRIM(@recipient_name))
  
    ; WITH CTE_Results AS   
    (  
        SELECT s.[id]
              ,s.[unique_id]
              ,[recipient_name]
              ,[price]
              ,[weight]
              ,[delivery_date_utc]
              ,[estimated_delivery_date_utc]
              ,[tracking_number]
              ,[is_active]
			  ,[route_id]
        From [dbo].[shipment] s
        WHERE  s.organisation_id = @org_id
        AND (@tracking_number IS NULL OR (@tracking_number IS NOT NULL AND s.[tracking_number] LIKE '%' + @tracking_number + '%' ))
        AND (@recipient_name IS NULL OR (@recipient_name IS NOT NULL AND s.[recipient_name] LIKE '%' + @recipient_name + '%' ))
        AND (@route_id  IS NULL OR (@route_id  IS NOT NULL AND s.route_id  = @route_id )) 
        AND s.is_deleted = 0
        ORDER BY  s.[id] desc
        OFFSET @PageSize * (@PageNo - 1) ROWS  
        FETCH NEXT @PageSize ROWS ONLY  
    ),  
    CTE_TotalRows AS   
    (  
        select count(id) as TotalRows from [dbo].[shipment] s 
        WHERE  s.organisation_id = @org_id
        AND (@tracking_number IS NULL OR (@tracking_number IS NOT NULL AND s.[tracking_number] LIKE '%' + @tracking_number + '%' ))
        AND (@recipient_name IS NULL OR (@recipient_name IS NOT NULL AND s.[recipient_name] LIKE '%' + @recipient_name + '%' ))
        AND (@route_id  IS NULL OR (@route_id  IS NOT NULL AND s.route_id  = @route_id )) 
        AND s.is_deleted = 0
    )  
    Select TotalRows,s.[id]
              ,s.[unique_id]
              ,s.[recipient_name]
              ,s.[price]
              ,s.[weight]
              ,s.[delivery_date_utc]
              ,s.[estimated_delivery_date_utc]
              ,s.[tracking_number]
              ,s.[is_active]
			  ,s.[route_id]
        From [dbo].[shipment] as s, CTE_TotalRows   
    WHERE EXISTS (SELECT 1 FROM CTE_Results WHERE CTE_Results.ID = s.id)  
  
    OPTION (RECOMPILE)  
END 