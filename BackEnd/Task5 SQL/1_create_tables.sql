-- Criando a tabela de Clientes
CREATE TABLE Customers (
    customer_id NUMBER PRIMARY KEY,
    name VARCHAR2(100) NOT NULL,
    email VARCHAR2(100) UNIQUE NOT NULL,
    created_date DATE DEFAULT SYSDATE
);

-- Criando a tabela de Pedidos
CREATE TABLE Orders (
    order_id NUMBER PRIMARY KEY,
    customer_id NUMBER REFERENCES Customers(customer_id),
    order_date DATE DEFAULT SYSDATE,
    total_amount NUMBER(10,2) CHECK (total_amount >= 0)
);

-- Criando a tabela de Produtos
CREATE TABLE Products (
    product_id NUMBER PRIMARY KEY,
    product_name VARCHAR2(100) NOT NULL,
    category VARCHAR2(50) NOT NULL
);

-- Criando a tabela de Itens do Pedido
CREATE TABLE Order_Items (
    order_item_id NUMBER PRIMARY KEY,
    order_id NUMBER REFERENCES Orders(order_id),
    product_id NUMBER REFERENCES Products(product_id),
    quantity NUMBER CHECK (quantity > 0),
    price NUMBER(10,2) CHECK (price >= 0)
);
