CREATE EXTENSION IF NOT EXISTS pgcrypto;

INSERT INTO "Items" ("Id", "Name", "Cost", "Effect", "Type")
VALUES
    (gen_random_uuid(), 'Hydraulic Jack', 150, 'Increase CPS by 0.5', 'Upgrade'),
    (gen_random_uuid(), 'Car Lift', 500, 'Increase CPS by 2', 'Upgrade'),
    (gen_random_uuid(), 'Power Drill', 300, 'Increase CPS by 1', 'Upgrade'),
    (gen_random_uuid(), 'Wrench Set', 100, 'Increase CPS by 0.2', 'Upgrade'),
    (gen_random_uuid(), 'Air Compressor', 400, 'Increase CPS by 1.5', 'Upgrade'),
    (gen_random_uuid(), 'Toolbox', 50, 'Increase CPS by 0.1', 'Upgrade'),
    (gen_random_uuid(), 'Garage Sign', 200, 'Cosmetic item', 'Cosmetic'),
    (gen_random_uuid(), 'Oil Can', 75, 'Increase CPS by 0.3', 'Upgrade'),
    (gen_random_uuid(), 'Workbench', 250, 'Increase CPS by 0.8', 'Upgrade'),
    (gen_random_uuid(), 'Tire Rack', 350, 'Increase CPS by 1.2', 'Upgrade');
