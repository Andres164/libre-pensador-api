PGDMP                         {            CafeLibrePensador    15.2    15.2                0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            	           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            
           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false                       1262    16398    CafeLibrePensador    DATABASE     �   CREATE DATABASE "CafeLibrePensador" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'English_United States.1252';
 #   DROP DATABASE "CafeLibrePensador";
                postgres    false            �            1259    16399    Cards    TABLE     ]   CREATE TABLE public."Cards" (
    card_id character(7) NOT NULL,
    customer_email bytea
);
    DROP TABLE public."Cards";
       public         heap    postgres    false                       0    0    TABLE "Cards"    ACL     ;   GRANT SELECT,UPDATE ON TABLE public."Cards" TO client_api;
          public          postgres    false    214            �            1259    16402 	   customers    TABLE     �   CREATE TABLE public.customers (
    loyverse_customer_id character varying(150) NOT NULL,
    date_of_birth bytea NOT NULL,
    email bytea NOT NULL
);
    DROP TABLE public.customers;
       public         heap    postgres    false                       0    0    TABLE customers    ACL     D   GRANT SELECT,INSERT,DELETE ON TABLE public.customers TO client_api;
          public          postgres    false    215            �            1259    24601 	   employees    TABLE     �   CREATE TABLE public.employees (
    is_admin boolean DEFAULT false NOT NULL,
    user_name bytea NOT NULL,
    password bytea NOT NULL
);
    DROP TABLE public.employees;
       public         heap    postgres    false                       0    0    TABLE employees    ACL     K   GRANT SELECT,INSERT,DELETE,UPDATE ON TABLE public.employees TO client_api;
          public          postgres    false    216                      0    16399    Cards 
   TABLE DATA           :   COPY public."Cards" (card_id, customer_email) FROM stdin;
    public          postgres    false    214   �                 0    16402 	   customers 
   TABLE DATA           O   COPY public.customers (loyverse_customer_id, date_of_birth, email) FROM stdin;
    public          postgres    false    215   S                 0    24601 	   employees 
   TABLE DATA           B   COPY public.employees (is_admin, user_name, password) FROM stdin;
    public          postgres    false    216   �       n           2606    16406    Cards Cards_pkey 
   CONSTRAINT     W   ALTER TABLE ONLY public."Cards"
    ADD CONSTRAINT "Cards_pkey" PRIMARY KEY (card_id);
 >   ALTER TABLE ONLY public."Cards" DROP CONSTRAINT "Cards_pkey";
       public            postgres    false    214            p           2606    24670    customers customers_email_key 
   CONSTRAINT     Y   ALTER TABLE ONLY public.customers
    ADD CONSTRAINT customers_email_key UNIQUE (email);
 G   ALTER TABLE ONLY public.customers DROP CONSTRAINT customers_email_key;
       public            postgres    false    215            r           2606    24676    customers customers_pkey 
   CONSTRAINT     Y   ALTER TABLE ONLY public.customers
    ADD CONSTRAINT customers_pkey PRIMARY KEY (email);
 B   ALTER TABLE ONLY public.customers DROP CONSTRAINT customers_pkey;
       public            postgres    false    215            t           2606    24686    employees employees_pkey 
   CONSTRAINT     ]   ALTER TABLE ONLY public.employees
    ADD CONSTRAINT employees_pkey PRIMARY KEY (user_name);
 B   ALTER TABLE ONLY public.employees DROP CONSTRAINT employees_pkey;
       public            postgres    false    216               �   x�E�Qn!C���T��1\���{~������J Q������]��Ԗ}Kn�-}K{�����*y�n8�-�24ͧO� ��^�[D�)�LYN^�/�[�\��W�;��k����?U�����<�Ⱦ�j��j����M"�����Z�d��� 3'������E����*��L:J�         +  x�5Q�m�0{ǽ8�\����H>jH�a�	�D���,\{�Şw�>ع��=���z}ec�xDЂ1�
�y��U#-wXW\~E��	�pFy%�?X9�L������*Ҫ�"ٚ;�kdv�KM~�H9XDG9g�#?��O.8&�&��Э�@_(݄tC�fD���:��4��v�s<{g߻?<��y��r�<�1+���G*�q�����Ԍ�K�T`�P-�C�B�o�'
S;K�/F0S��Y
���#�p��
���p�Q3�O�tp�����/��B�V�^�۶}{�z�         �   x�5�Qn� ����L$vz���PN�?���&�H��|�|]�Y��\$�72���%D&��?���33�`�8�O.y�����;���k�Ь׵�J�8$�W&��rD�O�ŧ6��]�Ɨ�����<�W���7�Vɢtg�7����y��#a�(����"��V4*���>��]�DR     