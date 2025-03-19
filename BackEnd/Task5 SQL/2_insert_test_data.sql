-- Inserir Clientes (8 clientes fictícios)
INSERT INTO Customers (customer_id, name, email, created_date) VALUES (1, 'John Doe', 'john.doe@email.com', ADD_MONTHS(SYSDATE, -5));
INSERT INTO Customers (customer_id, name, email, created_date) VALUES (2, 'Jane Smith', 'jane.smith@email.com', ADD_MONTHS(SYSDATE, -4));
INSERT INTO Customers (customer_id, name, email, created_date) VALUES (3, 'Alice Johnson', 'alice.j@email.com', ADD_MONTHS(SYSDATE, -3));
INSERT INTO Customers (customer_id, name, email, created_date) VALUES (4, 'Bob Brown', 'bob.b@email.com', ADD_MONTHS(SYSDATE, -6));
INSERT INTO Customers (customer_id, name, email, created_date) VALUES (5, 'Charlie White', 'charlie.w@email.com', ADD_MONTHS(SYSDATE, -2));
INSERT INTO Customers (customer_id, name, email, created_date) VALUES (6, 'Daniel Green', 'daniel.g@email.com', ADD_MONTHS(SYSDATE, -1));
INSERT INTO Customers (customer_id, name, email, created_date) VALUES (7, 'Emma Wilson', 'emma.w@email.com', ADD_MONTHS(SYSDATE, -3));
INSERT INTO Customers (customer_id, name, email, created_date) VALUES (8, 'Frank Black', 'frank.b@email.com', ADD_MONTHS(SYSDATE, -5));
INSERT INTO Customers (customer_id, name, email, created_date) VALUES (9, 'Grace Lee', 'grace.l@email.com', ADD_MONTHS(SYSDATE, -4));
INSERT INTO Customers (customer_id, name, email, created_date) VALUES (10, 'Henry Adams', 'henry.a@email.com', ADD_MONTHS(SYSDATE, -6));

-- Inserir Produtos
INSERT INTO Products (product_id, product_name, category) VALUES (1, 'Laptop', 'Eletrônicos');
INSERT INTO Products (product_id, product_name, category) VALUES (2, 'Smartphone', 'Eletrônicos');
INSERT INTO Products (product_id, product_name, category) VALUES (3, 'Fone de Ouvido', 'Acessórios');
INSERT INTO Products (product_id, product_name, category) VALUES (4, 'Monitor', 'Eletrônicos');
INSERT INTO Products (product_id, product_name, category) VALUES (5, 'Teclado Mecânico', 'Acessórios');

-- Inserir Pedidos (Clientes comprando coisas)
INSERT INTO Orders (order_id, customer_id, order_date, total_amount) VALUES (1, 1, ADD_MONTHS(SYSDATE, -2), 5000);
INSERT INTO Orders (order_id, customer_id, order_date, total_amount) VALUES (2, 2, ADD_MONTHS(SYSDATE, -3), 4500);
INSERT INTO Orders (order_id, customer_id, order_date, total_amount) VALUES (3, 3, ADD_MONTHS(SYSDATE, -1), 3200);
INSERT INTO Orders (order_id, customer_id, order_date, total_amount) VALUES (4, 4, ADD_MONTHS(SYSDATE, -4), 2900);
INSERT INTO Orders (order_id, customer_id, order_date, total_amount) VALUES (5, 5, ADD_MONTHS(SYSDATE, -2), 3800);
INSERT INTO Orders (order_id, customer_id, order_date, total_amount) VALUES (6, 6, ADD_MONTHS(SYSDATE, -5), 4100);
INSERT INTO Orders (order_id, customer_id, order_date, total_amount) VALUES (7, 7, ADD_MONTHS(SYSDATE, -6), 2700);
INSERT INTO Orders (order_id, customer_id, order_date, total_amount) VALUES (8, 8, ADD_MONTHS(SYSDATE, -1), 5200);
INSERT INTO Orders (order_id, customer_id, order_date, total_amount) VALUES (9, 9, ADD_MONTHS(SYSDATE, -3), 4800);
INSERT INTO Orders (order_id, customer_id, order_date, total_amount) VALUES (10, 10, ADD_MONTHS(SYSDATE, -4), 3500);

-- Inserir Itens nos Pedidos
INSERT INTO Order_Items (order_item_id, order_id, product_id, quantity, price) VALUES (1, 1, 1, 1, 2500);
INSERT INTO Order_Items (order_item_id, order_id, product_id, quantity, price) VALUES (2, 1, 2, 1, 2500);
INSERT INTO Order_Items (order_item_id, order_id, product_id, quantity, price) VALUES (3, 2, 3, 2, 1500);
INSERT INTO Order_Items (order_item_id, order_id, product_id, quantity, price) VALUES (4, 2, 4, 1, 3000);
INSERT INTO Order_Items (order_item_id, order_id, product_id, quantity, price) VALUES (5, 3, 5, 3, 1000);
INSERT INTO Order_Items (order_item_id, order_id, product_id, quantity, price) VALUES (6, 4, 1, 1, 2900);
INSERT INTO Order_Items (order_item_id, order_id, product_id, quantity, price) VALUES (7, 5, 2, 2, 1900);
INSERT INTO Order_Items (order_item_id, order_id, product_id, quantity, price) VALUES (8, 6, 3, 1, 1000);
INSERT INTO Order_Items (order_item_id, order_id, product_id, quantity, price) VALUES (9, 6, 4, 2, 1500);
INSERT INTO Order_Items (order_item_id, order_id, product_id, quantity, price) VALUES (10, 7, 5, 1, 2700);
INSERT INTO Order_Items (order_item_id, order_id, product_id, quantity, price) VALUES (11, 8, 1, 2, 2600);
INSERT INTO Order_Items (order_item_id, order_id, product_id, quantity, price) VALUES (12, 9, 2, 3, 1600);
INSERT INTO Order_Items (order_item_id, order_id, product_id, quantity, price) VALUES (13, 10, 3, 2, 1750);

COMMIT;