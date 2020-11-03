--  Adds support for creating UUIDs if it doesn't already exist
CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

--  Creates based on the MAC address of the server
SELECT uuid_generate_v1();

--  Creates based on completely random (as far as computers can do random) numbers
SELECT uuid_generate_v4();

