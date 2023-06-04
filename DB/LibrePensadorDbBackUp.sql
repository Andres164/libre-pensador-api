PGDMP         4                {            CafeLibrePensador    15.2    15.2                0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
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
          public          postgres    false    215            �            1259    24601    users    TABLE     �   CREATE TABLE public.users (
    is_admin boolean DEFAULT false NOT NULL,
    password bytea NOT NULL,
    user_name character varying(60) NOT NULL
);
    DROP TABLE public.users;
       public         heap    postgres    false                       0    0    TABLE users    ACL     G   GRANT SELECT,INSERT,DELETE,UPDATE ON TABLE public.users TO client_api;
          public          postgres    false    216                      0    16399    Cards 
   TABLE DATA           :   COPY public."Cards" (card_id, customer_email) FROM stdin;
    public          postgres    false    214   f                 0    16402 	   customers 
   TABLE DATA           O   COPY public.customers (loyverse_customer_id, date_of_birth, email) FROM stdin;
    public          postgres    false    215   t                 0    24601    users 
   TABLE DATA           >   COPY public.users (is_admin, password, user_name) FROM stdin;
    public          postgres    false    216   >       n           2606    16406    Cards Cards_pkey 
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
       public            postgres    false    215            t           2606    25891    users users_pkey 
   CONSTRAINT     U   ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pkey PRIMARY KEY (user_name);
 :   ALTER TABLE ONLY public.users DROP CONSTRAINT users_pkey;
       public            postgres    false    216               �   x�EP[n�0�n3�&Eٗ�}�Ǎ�3��c�1��� >��)>z����y�~ø!n�������8�d�7���Uu���Ɋ���
fR()&�䚙�$W6O'�7�D��<��JtBŪ���������|+f��o�Q��`��n�ڑˎ/Wg�^WCֱ`�A)t��V�.���Q㩆�*O�����_�ə#�c7}Լ5jDQ�K/�[���Q�
x��kI��jά<�1�ͨ��!eG��r����k۶E�j�         �  x�ERI��@<���p�/uɍ7��'��������n��t;}���3��m�&�^��^�X@^f)��2ð}������-Ӗ-].!��iZ�]iy�����@Og_�t<(�w4�Ȕ��,�m�+�W ��N���2.�B�;{ȧ���͖!g75!���⩤ u xb�5�k�f�l1�bJ���t���ڧ�9��y(�#Xz��s��� "=�٥#�/�����@wV	6�3����t�����	/�� ��r�k
��E!�5&;n:S�ȍ�
�pe5�ݝAC�A9J6�^��Oy6g���Fz�o�xg4��ŋR�!�\Q�5�����&x����SP������0�a����&��̏�SW�Ě��K
�hL�������a�TL?�Sn�3&�f�W2��dVw꼒����MU���1���8�+Ǿ�         y   x��;�0D��0(���:w�n�CJA@���i����K)�$H�N��tjӇNnec4Q�S�U�J�Jj��6)�AF\�j6/^�x�Y�������v��<>u��:���-����%     