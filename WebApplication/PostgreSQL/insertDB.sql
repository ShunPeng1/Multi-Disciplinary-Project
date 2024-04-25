SELECT * FROM controls;

SELECT * FROM device;

SELECT * FROM log_record;

SELECT * FROM sensor_data;

SELECT * FROM users;

INSERT INTO users (username, u_password, email, u_salt)
VALUES
	('thuan1name', 'thuan1pssw', 'thuan1email', '12'),
	('thuan2name', 'thuan2pssw', 'thuan2email', '12'),
	('thuan3name', 'thuan3pssw', 'thuan3email', '12'),
	('thuan4name', 'thuan4pssw', 'thuan4email', '12'),
	('thuan5name', 'thuan5pssw', 'thuan5email', '12')
RETURNING *;

INSERT INTO log_record (l_timestmp, activity, l_username_fk)
VALUES
	('01/01/2024 1:00:00', 'updating', 'thuan1name'),
	('02/02/2024 2:00:00', 'debugging', 'thuan2name'),
	('03/03/2024 3:00:00', 'coding', 'thuan3name'),
	('04/04/2024 4:00:00', 'playing', 'thuan4name'),
	('05/05/2024 5:00:00', 'sleeping', 'thuan5name')
RETURNING *;

INSERT INTO device (d_name)
VALUES
	('TV'),
	('Fan'),
	('Light'),
	('AC'),
	('Speaker')
RETURNING *;

INSERT INTO sensor_data (s_timestmp, s_value, s_did_fk)
VALUES
	('01/01/2020 1:00:00', '10101010', 1),
	('02/02/2020 2:00:00', '20101010', 2),
	('03/03/2020 3:00:00', '30101010', 3),
	('04/04/2020 4:00:00', '40101010', 4),
	('05/05/2020 5:00:00', '50101010', 5)
RETURNING *;

INSERT INTO controls (c_username, c_did, recurrence, start_time, end_time)
VALUES
	('thuan1name', 1, '1','09:30:00','09:30:00'),
	('thuan2name', 2, '2','09:30:00','09:31:00'),
	('thuan3name', 3, '3','09:30:00','10:30:00'),
	('thuan4name', 4, '4','09:30:00','17:30:00'),
	('thuan5name', 5, '5','09:30:00','08:30:00')  --Raise trigger here, start_time > end_time
RETURNING *;

