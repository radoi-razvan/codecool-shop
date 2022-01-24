--DROP TABLE IF EXISTS cart_product;
--DROP TABLE IF EXISTS cartproduct;
--DROP TABLE IF EXISTS order_product;
--DROP TABLE IF EXISTS product;
--DROP TABLE IF EXISTS supplier;
--DROP TABLE IF EXISTS category;
--DROP TABLE IF EXISTS cart;
--DROP TABLE IF EXISTS client_order;
--DROP TABLE IF EXISTS account;


CREATE TABLE supplier (
    id int IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(20) NOT NULL,
    description VARCHAR(250) NOT NULL,
);

CREATE TABLE category (
    id int IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(20) NOT NULL,
	department VARCHAR(20) NOT NULL,
    description VARCHAR(500) NOT NULL,
);

CREATE TABLE product (
    id int IDENTITY(1,1) PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
	defaultprice DECIMAL(10, 2) NOT NULL,
    description VARCHAR(500) NOT NULL,
	currency VARCHAR(10) NOT NULL,
	supplier_id int,
	FOREIGN KEY (supplier_id) REFERENCES supplier(id),
	category_id int,
	FOREIGN KEY (category_id) REFERENCES category(id),
);

--CREATE TABLE account (
--	id int IDENTITY(1,1) PRIMARY KEY,
--	username VARCHAR(18) NOT NULL,
--	email VARCHAR(100) NOT NULL,
--	password NVARCHAR(100) NOT NULL,
--);

CREATE TABLE cart (
	id int IDENTITY(1,1) PRIMARY KEY,
	account_id nvarchar(450),
	FOREIGN KEY (account_id) REFERENCES AspNetUsers(Id),
);

CREATE TABLE cart_product (
	cart_id int,
	FOREIGN KEY (cart_id) REFERENCES cart(id), 
	product_id int,
	FOREIGN KEY (product_id) REFERENCES product(id),
	quantity int,
);

CREATE TABLE client_order (
	id int IDENTITY(1,1) PRIMARY KEY,
	account_id nvarchar(450),
	FOREIGN KEY (account_id) REFERENCES AspNetUsers(id),
	order_status VARCHAR(100) NOT NULL,
	first_name VARCHAR(100) NOT NULL,
	last_name VARCHAR(100) NOT NULL,
	client_email VARCHAR(100) NOT NULL,
	client_address VARCHAR(100) NOT NULL,
	phone_number VARCHAR(50) NOT NULL,
	country VARCHAR(50) NOT NULL,
	city VARCHAR(50) NOT NULL,
	zip_code VARCHAR(10) NOT NULL,
	order_date DATETIME,
);

CREATE TABLE order_product (
	client_order_id int,
	FOREIGN KEY (client_order_id) REFERENCES client_order(id), 
	product_id int,
	FOREIGN KEY (product_id) REFERENCES product(id),
	quantity int,
);

INSERT INTO category(name, department, description) VALUES
	('Tablet', 'Hardware', 'A tablet computer, commonly shortened to tablet, is a thin, flat mobile computer with a touchscreen display.'),
	('Laptop', 'Hardware', 'A small portable personal computer with a screen and alphanumeric keyboard.'),
	('Phone', 'Hardware', 'A telecommunications device that permits two or more users to conduct a conversation when they are too far apart to be heard directly.'),
	('Tv', 'Electonics', 'A telecommunication medium used for transmitting moving images in monochrome (black and white), or in color, and in two or three dimensions and sound.'),
	('Refrigerator', 'Appliances', 'that transfers heat from its inside to its external environment so that its inside is cooled to a temperature below the room temperature.'),
	('Stove', 'Appliances', 'A device that burns fuel or uses electricity to generate heat inside or on top of the apparatus.'),
	('Washing Machine', 'Appliances', 'A home appliance used to wash laundry.')
;

INSERT INTO supplier (name, description) VALUES
    ('Amazon', 'Digital content and services'),
    ('Lenovo', 'Computers'),
	('Asus','Computers'),
	('Alienware','Computers'),
	('Apple','Devices'),
	('Samsung','Devices'),
	('Sony','Devices'),
	('Miele','Refrigerators'),
	('Liebherr','Refrigerators'),
	('Bosch','Different types of devices'),
	('Primer','Washing machines'),
	('Moratti','Stoves')
;

	INSERT INTO product(name, defaultprice, currency, description, category_id, supplier_id) VALUES
	('Amazon Fire', 49.9,'USD' ,'Fantastic price. Large content ecosystem. Good parental controls. Helpful technical support', 
	(SELECT id FROM category WHERE name = 'Tablet'), (SELECT id FROM supplier WHERE name = 'Amazon')),
	('Lenovo IdeaPad Miix 700', 479,'USD' ,'Keyboard cover is included. Fanless Core m5 processor. Full-size USB ports. Adjustable kickstand', 
	(SELECT id FROM category WHERE name = 'Tablet'), (SELECT id FROM supplier WHERE name = 'Lenovo')),
	('Amazon Fire HD 8', 89,'USD' ,'Amazon''s latest Fire HD 8 tablet is a great value for media consumption', 
	(SELECT id FROM category WHERE name = 'Tablet'), (SELECT id FROM supplier WHERE name = 'Amazon')),
	('Laptop Gaming ASUS TUF A17 FA706IH', 989.9,'USD' ,'Ready for very serious gaming and extreme durability, a high-performance gaming laptop', 
	(SELECT id FROM category WHERE name = 'Laptop'), (SELECT id FROM supplier WHERE name = 'Amazon')),
	('Laptop Lenovo IdeaPad Gaming 3', 575,'USD' ,'Smoothly play your favorite games with NVIDIA GeForce GTX graphics', 
	(SELECT id FROM category WHERE name = 'Laptop'), (SELECT id FROM supplier WHERE name = 'Lenovo')),
	('Laptop ASUS ProArt StudioBook One W590G6T', 8000,'USD' ,'The first laptop equipped with the NVIDIA Quadro RTX 6000 graphics solution, one of the most powerful StudioBook model ever created', 
	(SELECT id FROM category WHERE name = 'Laptop'), (SELECT id FROM supplier WHERE name = 'Asus')),
	('Laptop Apple MacBook Pro 15', 5999.9,'USD' ,'Stronger. More efficient. Better experience', 
	(SELECT id FROM category WHERE name = 'Laptop'), (SELECT id FROM supplier WHERE name = 'Apple')),
	('Laptop Gaming Alienware Area 51M R2', 5898,'USD' ,'With extraordinary desktop processing power', 
	(SELECT id FROM category WHERE name = 'Laptop'), (SELECT id FROM supplier WHERE name = 'Alienware')),
	('Iphone 12 PRO mobile phone', 3050.9,'USD' ,'Customized by platinum plating, made by Vip Touch Design Dubai, a manufacture specialized in producing exclusive gadgets', 
	(SELECT id FROM category WHERE name = 'Phone'), (SELECT id FROM supplier WHERE name = 'Apple')),
	('Samsung Galaxy S9 mobile phone', 2680,'USD' ,'Discover a new world with the Samsung Galaxy S9', 
	(SELECT id FROM category WHERE name = 'Phone'), (SELECT id FROM supplier WHERE name = 'Samsung')),
	('Sony Xperia 1 III mobile phone', 1180,'USD' ,'Xperia 1 III is a sign of effort in the Sony brand team', 
	(SELECT id FROM category WHERE name = 'Phone'), (SELECT id FROM supplier WHERE name = 'Sony')),
	('Samsung 98Q950RB TV', 6239.9,'USD' ,'Samsung QLED 8K Q950R will take you into the depths of reality to a higher level', 
	(SELECT id FROM category WHERE name = 'Tv'), (SELECT id FROM supplier WHERE name = 'Samsung')),
	('Sony Bravia FW-100BZ40J Professional TV', 1519.8,'USD' ,'100 inch, 4K Ultra HD, LED', 
	(SELECT id FROM category WHERE name = 'Tv'), (SELECT id FROM supplier WHERE name = 'Sony')),
	('Sony 85Z9J TV', 999.8,'USD' ,'The BRAVIA XR TV takes the picture and sound to the next level with the ingenious Cognitive Processor XR', 
	(SELECT id FROM category WHERE name = 'Tv'), (SELECT id FROM supplier WHERE name = 'Sony')),
	('Refrigerator with one door MIELE KS 28463 D BB', 3236.8,'USD' ,'PerfectFresh Pro, FlexiLight, DynaCool, ComfortClean', 
	(SELECT id FROM category WHERE name = 'Refrigerator'), (SELECT id FROM supplier WHERE name = 'Miele')),
	('Refrigerator LIEBHERR Kef 4330 Comfort', 2760,'USD' ,'More volume inside for food, spacious and energy efficient during operation', 
	(SELECT id FROM category WHERE name = 'Refrigerator'), (SELECT id FROM supplier WHERE name = 'Liebherr')),
	('Refrigerator with a door Bosch KSV36AI3P', 1428,'USD' ,'LED lighting: keep the contents of your refrigerator in the right light', 
	(SELECT id FROM category WHERE name = 'Refrigerator'), (SELECT id FROM supplier WHERE name = 'Bosch')),
	('Professional washing machine Primer LP-10T2', 3659.2,'USD' ,'Possibility to program, export and import unlimited programs through the USB port', 
	(SELECT id FROM category WHERE name = 'Washing Machine'), (SELECT id FROM supplier WHERE name = 'Primer')),
	('Front washing machine MIELE WEI 875 WPS', 1598,'USD' ,'QuickPowerWash washing programs, Automatic Plus, Cotton, Minimal ironing', 
	(SELECT id FROM category WHERE name = 'Washing Machine'), (SELECT id FROM supplier WHERE name = 'Primer')),
	('Miele WEG675 WPS washing machine', 1269.8,'USD' ,'Delay start, Timer, Automatic load recognition, Self-dosing', 
	(SELECT id FROM category WHERE name = 'Washing Machine'), (SELECT id FROM supplier WHERE name = 'Miele')),
	('Moratti professional stove, 6 mesh 1200x900x850 mm', 3630.4,'USD' ,'Gas supply, Power 39 kW, Weight 205 kg', 
	(SELECT id FROM category WHERE name = 'Stove'), (SELECT id FROM supplier WHERE name = 'Moratti')),
	('4-burner cooking stove, gas, 900x900x850 mm Moratti', 828.4,'USD' ,'Stainless steel body with cast iron grills, Safety thermocouple', 
	(SELECT id FROM category WHERE name = 'Stove'), (SELECT id FROM supplier WHERE name = 'Moratti')),
	('Bosch HKS59D250 cooker', 579.8,'USD' ,'With AutoPilot automatic programs you automatically get the best results', 
	(SELECT id FROM category WHERE name = 'Stove'), (SELECT id FROM supplier WHERE name = 'Primer'))
	;

--INSERT INTO account(username, email, password) VALUES ('Bob', 'bob_doe@gmail.com', 'bob_password');
--INSERT INTO cart(account_id) VALUES (1);