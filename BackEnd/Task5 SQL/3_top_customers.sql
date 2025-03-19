SET SERVEROUTPUT ON;

DECLARE
    -- Cursor para buscar os 5 clientes que mais gastaram nos últimos 6 meses
    CURSOR top_customers_cursor IS
        SELECT c.customer_id, c.name, SUM(o.total_amount) AS total_spent
        FROM Customers c
        JOIN Orders o ON c.customer_id = o.customer_id
        WHERE o.order_date >= ADD_MONTHS(SYSDATE, -6)
        GROUP BY c.customer_id, c.name
        ORDER BY total_spent DESC
        FETCH FIRST 5 ROWS ONLY;

    -- Variáveis para armazenar os dados do cursor
    v_customer_id Customers.customer_id%TYPE;
    v_name Customers.name%TYPE;
    v_total_spent Orders.total_amount%TYPE;

    -- Cursor para buscar os produtos comprados por um cliente
    CURSOR products_cursor(p_customer_id NUMBER) IS
        SELECT p.product_name, SUM(oi.quantity) AS total_quantity
        FROM Order_Items oi
        JOIN Orders o ON oi.order_id = o.order_id
        JOIN Products p ON oi.product_id = p.product_id
        WHERE o.customer_id = p_customer_id
        GROUP BY p.product_name;

    -- Variáveis para armazenar os produtos
    v_product_name Products.product_name%TYPE;
    v_total_quantity Order_Items.quantity%TYPE;
BEGIN
    DBMS_OUTPUT.PUT_LINE('⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠');
    DBMS_OUTPUT.PUT_LINE('  Top 5 Clientes que Mais Gastaram ');
    DBMS_OUTPUT.PUT_LINE('⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠⚠');

    -- Abrindo o cursor principal
    FOR customer_rec IN top_customers_cursor LOOP
        v_customer_id := customer_rec.customer_id;
        v_name := customer_rec.name;
        v_total_spent := customer_rec.total_spent;

        -- Exibir dados do cliente
        DBMS_OUTPUT.PUT_LINE('Cliente ID: ' || v_customer_id);
        DBMS_OUTPUT.PUT_LINE('Nome: ' || v_name);
        DBMS_OUTPUT.PUT_LINE('Total Gasto: $' || v_total_spent);
        DBMS_OUTPUT.PUT_LINE('Produtos Comprados:');

        -- Abrindo o cursor de produtos comprados pelo cliente
        FOR product_rec IN products_cursor(v_customer_id) LOOP
            v_product_name := product_rec.product_name;
            v_total_quantity := product_rec.total_quantity;
            DBMS_OUTPUT.PUT_LINE('    - ' || v_product_name || ': ' || v_total_quantity || ' unidades');
        END LOOP;

        DBMS_OUTPUT.PUT_LINE('--------------------------------------');
    END LOOP;
END;
/
