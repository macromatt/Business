-- No need for UPPER() calls in NuoDB
ALTER TABLE Culture ALTER COLUMN name COLLATE case_insensitive;
ALTER TABLE Word ALTER COLUMN value COLLATE case_insensitive;
CREATE INDEX Word_culture_value ON Word (culture,value);
CREATE INDEX Word_value ON Word (value);

DROP INDEX SENTENCE_CULTURE_VALUE;
ALTER TABLE Sentence ALTER COLUMN value COLLATE case_insensitive;
CREATE INDEX Sentence_culture_value ON Sentence(culture,value);
CREATE INDEX Sentence_value ON Sentence(value);

ALTER TABLE Country ALTER COLUMN code COLLATE case_insensitive;

DROP INDEX POSTAL_COUNTRY_CODE;
ALTER TABLE Postal ALTER COLUMN code COLLATE case_insensitive;
CREATE INDEX Postal_country_code ON Postal (country,code);

ALTER TABLE Path ALTER COLUMN host COLLATE case_insensitive;


-- Session function indexes, untested
CREATE INDEX sessionCredentialFull ON SessionCredential (session,agentstring,credential,referring,fromaddress,location);
CREATE INDEX sessionToken_token_siteApplicationRelease ON SessionToken (token, siteApplicationRelease);
CREATE INDEX Path_protocol_secure_host_null_null ON Path(protocol,secure,host);

-- Need to investigate null handling in indexes
CREATE INDEX version_major_minor_patch_null ON Version(major,minor,patch);
CREATE INDEX version_name_major_minor_null ON Version(name,major,minor);
CREATE INDEX version_null_major_minor_null ON Version(major,minor);
CREATE INDEX version_name_null_null_null ON Version(name);
CREATE INDEX AssemblyApplicationRelease_assembly_applicationRelease_null ON AssemblyApplicationRelease(assembly,applicationRelease);
CREATE INDEX Part_name_null_null_null ON Part(name);
CREATE INDEX sessionCredential_session_as_fa ON SessionCredential (session,agentstring,fromaddress);
CREATE INDEX cultureName ON Culture(name);
CREATE INDEX Location_parent_marquee_longitude_latitude_accuracy_level_altitudeabovesealevel_area ON Location (parent,marquee,longitude,latitude,accuracy,level,altitudeabovesealevel,area);
CREATE INDEX Location_longitude_latitude_level ON Location (longitude,latitude,level);
CREATE INDEX Release_version_null ON Release (version);
CREATE INDEX ApplicationRelease_application_null ON ApplicationRelease (application);
CREATE INDEX Path_protocol_secure_host_value_null ON Path(protocol,secure,host,value);


-- Automatically created Sequences
ALTER SEQUENCE WORD$IDENTITY_SEQUENCE START WITH 2000000;
ALTER SEQUENCE SENTENCE$IDENTITY_SEQUENCE START WITH 2000000;