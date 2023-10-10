--liquibase formatted sql

--changeset mikebarton:20210808-3-data-a
--rollback delete from CategoryFilterGroup where `Name` = 'Price'
insert into CategoryFilterGroup (`Name`, CategoryId, FilterType, `Order`, FilterCode, MinLabel, MaxLabel)
select 'Price', t.CategoryId, 0, 0, 'price-filter', 'Min', 'Max' from (select distinct CategoryId from Category) t;

--changeset mikebarton:20210808-1-data-b
--rollback delete from CategoryFilterGroup where `Name` = 'Colour'
insert into CategoryFilterGroup (`Name`, CategoryId, FilterType, `Order`, FilterCode)
select 'Colour', t.CategoryId, 1, 1, 'colour-filter' from (select distinct CategoryId from CategoryFilter where FilterTypeCode = 'COL') t;

--changeset mikebarton:20210808-1-data-c
--rollback delete from CategoryFilterGroup where `Name` = 'Size'
insert into CategoryFilterGroup (`Name`, CategoryId, FilterType, `Order`, FilterCode)
select 'Size', t.CategoryId, 1, 2, 'size-filter' from (select distinct CategoryId from CategoryFilter where FilterTypeCode = 'SIZ') t;

--changeset mikebarton:20210808-1-data-d
--rollback delete from CategoryFilterGroup where `Name` = 'Gender'
insert into CategoryFilterGroup (`Name`, CategoryId, FilterType, `Order`, FilterCode)
select 'Gender', t.CategoryId, 1, 3, 'gender-filter' from (select distinct CategoryId from CategoryFilter where FilterTypeCode = 'GEN') t;

--changeset mikebarton:20210808-1-data-e
--rollback delete from CategoryFilterGroup where `Name` = 'Gender'
insert into CategoryFilterGroup (`Name`, CategoryId, FilterType, `Order`, FilterCode)
select 'Brand', t.CategoryId, 1, 4, 'brand-filter' from (select distinct CategoryId from Category) t;

--changeset mikebarton:20210808-1-data-f
--rollback delete from CategoryFilterGroup where `Name` = 'Gender'
insert into CategoryFilterGroup (`Name`, CategoryId, FilterType, `Order`, FilterCode)
select 'Seller', t.CategoryId, 1, 5, 'merchant-filter' from (select distinct CategoryId from Category) t;