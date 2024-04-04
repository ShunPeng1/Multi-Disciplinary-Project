BEGIN;

CREATE TABLE IF NOT EXISTS users
(
	username VARCHAR(30) PRIMARY KEY,
	u_password VARCHAR(255) NOT NULL,
	u_salt VARCHAR(255) NOT NULL,
	email VARCHAR(30) UNIQUE,
	fname VARCHAR(10),
	lname VARCHAR(10)
);

CREATE TABLE IF NOT EXISTS log_record
(
	l_timestmp TIMESTAMP PRIMARY KEY DEFAULT NOW(),
	activity VARCHAR(30) NOT NULL,
	l_username_fk VARCHAR(30),
	CONSTRAINT fk_log_record
		FOREIGN KEY(l_username_fk)
			REFERENCES users(username)
);

CREATE TABLE IF NOT EXISTS device
(
	device_id UUID PRIMARY KEY DEFAULT gen_random_uuid(),
	d_name VARCHAR(30) NOT NULL,
	d_location VARCHAR(30),
	d_type VARCHAR(10),
	d_state VARCHAR(10)
);

CREATE TABLE IF NOT EXISTS sensor_data
(
	s_timestmp TIMESTAMP PRIMARY KEY DEFAULT NOW(),
	s_value VARCHAR(50) NOT NULL,
	s_did_fk UUID,
	CONSTRAINT fk_sensor_data
		FOREIGN KEY(s_did_fk)
			REFERENCES device(device_id)
);

CREATE TABLE IF NOT EXISTS controls
(
	c_username VARCHAR(30),
	c_did UUID,
	recurrence VARCHAR(30),
	start_time TIME,
	end_time TIME,
	CONSTRAINT pk_controls PRIMARY KEY (c_username,c_did),
	CONSTRAINT fk_controls_1
		FOREIGN KEY(c_username)
			REFERENCES users(username),
	CONSTRAINT fk_controls_2
		FOREIGN KEY(c_did)
			REFERENCES device(device_id)
);

END;