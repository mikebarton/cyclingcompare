--liquibase formatted sql

--changeset mikebarton:20210808-2-data-a
--rollback update CategoryFilter cf set cf.CategoryFilterGroupId = '-1' where cf.FilterTypeCode = 'COL'
update CategoryFilter cf 
set cf.CategoryFilterGroupId = (select cfg.CategoryFilterGroupId from CategoryFilterGroup cfg where cfg.CategoryId = cf.CategoryId and cfg.Name = 'Colour' limit 1)
where cf.FilterTypeCode = 'COL';

--changeset mikebarton:20210808-2-data-b
--rollback update CategoryFilter cf set cf.CategoryFilterGroupId = '-1' where cf.FilterTypeCode = 'SIZ'
update CategoryFilter cf 
set cf.CategoryFilterGroupId = (select cfg.CategoryFilterGroupId from CategoryFilterGroup cfg where cfg.CategoryId = cf.CategoryId and cfg.Name = 'Size' limit 1)
where cf.FilterTypeCode = 'SIZ';

--changeset mikebarton:20210808-2-data-c
--rollback update CategoryFilter cf set cf.CategoryFilterGroupId = '-1' where cf.FilterTypeCode = 'GEN'
update CategoryFilter cf 
set cf.CategoryFilterGroupId = (select cfg.CategoryFilterGroupId from CategoryFilterGroup cfg where cfg.CategoryId = cf.CategoryId and cfg.Name = 'Gender' limit 1)
where cf.FilterTypeCode = 'GEN';