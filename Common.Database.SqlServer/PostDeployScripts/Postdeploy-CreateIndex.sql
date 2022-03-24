
CREATE NONCLUSTERED INDEX idx_user_account_email_username ON user_account(username,email);
CREATE NONCLUSTERED INDEX idx_user_account_id ON user_account(id);
CREATE NONCLUSTERED INDEX idx_tracking_device_events ON tracking_device_events([event],[imie],[status],[created_utc]);
CREATE NONCLUSTERED INDEX idx_tracking_device_events_imie_st ON tracking_device_events([imie],[status],[created_utc]);

CREATE NONCLUSTERED INDEX idx_tracker_imie_st ON tracker(imie,is_active);

CREATE NONCLUSTERED INDEX idx_shipment ON [shipment]([delivery_date_utc],[estimated_delivery_date_utc],[organisation_id],route_id,tracker_id);

CREATE NONCLUSTERED INDEX idx_shipment_tracking_subq ON [shipment]([organisation_id],is_deleted,is_active);

CREATE NONCLUSTERED INDEX idx_tracking_shipment_tracking ON shipment_tracking([shipment_id],[created_utc]);

CREATE NONCLUSTERED INDEX idx_route_orgin_dest ON dbo.[route]([origin],[destination]);

CREATE NONCLUSTERED INDEX idx_shipments_name_track_org_route ON dbo.[shipment](tracking_number,recipient_name,[organisation_id],route_id);

CREATE FULLTEXT CATALOG ftCat_haulage as default

CREATE FULLTEXT INDEX ON dbo.[route]([origin],[destination]) 
KEY INDEX PK_route 
WITH STOPLIST = SYSTEM;

CREATE FULLTEXT INDEX ON dbo.[shipment](tracking_number,recipient_name,recipient_address,recipient_phone) 
KEY INDEX PK_shipment 
WITH STOPLIST = SYSTEM;



GO