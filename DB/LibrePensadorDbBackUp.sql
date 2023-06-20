PGDMP     9    3    	            {            CafeLibrePensador    15.2    15.2 )    )           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            *           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            +           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            ,           1262    16398    CafeLibrePensador    DATABASE     �   CREATE DATABASE "CafeLibrePensador" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'English_United States.1252';
 #   DROP DATABASE "CafeLibrePensador";
                postgres    false            �            1259    16399    cards    TABLE     [   CREATE TABLE public.cards (
    card_id character(7) NOT NULL,
    customer_email bytea
);
    DROP TABLE public.cards;
       public         heap    postgres    false            -           0    0    TABLE cards    ACL     9   GRANT SELECT,UPDATE ON TABLE public.cards TO client_api;
          public          postgres    false    214            �            1259    16402 	   customers    TABLE     �   CREATE TABLE public.customers (
    loyverse_customer_id character varying(150) NOT NULL,
    date_of_birth bytea NOT NULL,
    email bytea NOT NULL
);
    DROP TABLE public.customers;
       public         heap    postgres    false            .           0    0    TABLE customers    ACL     D   GRANT SELECT,INSERT,DELETE ON TABLE public.customers TO client_api;
          public          postgres    false    215            �            1259    25955    expense_categories    TABLE        CREATE TABLE public.expense_categories (
    category_id integer NOT NULL,
    category_name character varying(40) NOT NULL
);
 &   DROP TABLE public.expense_categories;
       public         heap    postgres    false            /           0    0    TABLE expense_categories    ACL     T   GRANT SELECT,INSERT,DELETE,UPDATE ON TABLE public.expense_categories TO client_api;
          public          postgres    false    220            �            1259    25954 "   expense_categories_category_id_seq    SEQUENCE     �   ALTER TABLE public.expense_categories ALTER COLUMN category_id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.expense_categories_category_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    220            �            1259    25963    expenses    TABLE       CREATE TABLE public.expenses (
    expense_id integer NOT NULL,
    type integer NOT NULL,
    importance integer NOT NULL,
    category_id integer NOT NULL,
    amount_spent numeric(8,2) NOT NULL,
    date timestamp with time zone NOT NULL,
    description text NOT NULL
);
    DROP TABLE public.expenses;
       public         heap    postgres    false            0           0    0    TABLE expenses    ACL     J   GRANT SELECT,INSERT,DELETE,UPDATE ON TABLE public.expenses TO client_api;
          public          postgres    false    222            �            1259    25962    expenses_expense_id_seq    SEQUENCE     �   ALTER TABLE public.expenses ALTER COLUMN expense_id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.expenses_expense_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    222            �            1259    25898    log_entries    TABLE     �   CREATE TABLE public.log_entries (
    id integer NOT NULL,
    log_level character varying(50) NOT NULL,
    message text NOT NULL,
    exception text,
    stack_trace text,
    occurred_on timestamp without time zone DEFAULT now() NOT NULL
);
    DROP TABLE public.log_entries;
       public         heap    postgres    false            1           0    0    TABLE log_entries    ACL     ?   GRANT SELECT,INSERT ON TABLE public.log_entries TO client_api;
          public          postgres    false    218            �            1259    25897    log_entries_id_seq    SEQUENCE     �   CREATE SEQUENCE public.log_entries_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 )   DROP SEQUENCE public.log_entries_id_seq;
       public          postgres    false    218            2           0    0    log_entries_id_seq    SEQUENCE OWNED BY     I   ALTER SEQUENCE public.log_entries_id_seq OWNED BY public.log_entries.id;
          public          postgres    false    217            3           0    0    SEQUENCE log_entries_id_seq    ACL     H   GRANT SELECT,USAGE ON SEQUENCE public.log_entries_id_seq TO client_api;
          public          postgres    false    217            �            1259    24601    users    TABLE     �   CREATE TABLE public.users (
    is_admin boolean DEFAULT false NOT NULL,
    password bytea NOT NULL,
    user_name character varying(60) NOT NULL
);
    DROP TABLE public.users;
       public         heap    postgres    false            4           0    0    TABLE users    ACL     G   GRANT SELECT,INSERT,DELETE,UPDATE ON TABLE public.users TO client_api;
          public          postgres    false    216            |           2604    25901    log_entries id    DEFAULT     p   ALTER TABLE ONLY public.log_entries ALTER COLUMN id SET DEFAULT nextval('public.log_entries_id_seq'::regclass);
 =   ALTER TABLE public.log_entries ALTER COLUMN id DROP DEFAULT;
       public          postgres    false    218    217    218                      0    16399    cards 
   TABLE DATA           8   COPY public.cards (card_id, customer_email) FROM stdin;
    public          postgres    false    214   -                 0    16402 	   customers 
   TABLE DATA           O   COPY public.customers (loyverse_customer_id, date_of_birth, email) FROM stdin;
    public          postgres    false    215   D-       $          0    25955    expense_categories 
   TABLE DATA           H   COPY public.expense_categories (category_id, category_name) FROM stdin;
    public          postgres    false    220   a-       &          0    25963    expenses 
   TABLE DATA           n   COPY public.expenses (expense_id, type, importance, category_id, amount_spent, date, description) FROM stdin;
    public          postgres    false    222   �-       "          0    25898    log_entries 
   TABLE DATA           b   COPY public.log_entries (id, log_level, message, exception, stack_trace, occurred_on) FROM stdin;
    public          postgres    false    218   �-                  0    24601    users 
   TABLE DATA           >   COPY public.users (is_admin, password, user_name) FROM stdin;
    public          postgres    false    216   1       5           0    0 "   expense_categories_category_id_seq    SEQUENCE SET     P   SELECT pg_catalog.setval('public.expense_categories_category_id_seq', 8, true);
          public          postgres    false    219            6           0    0    expenses_expense_id_seq    SEQUENCE SET     F   SELECT pg_catalog.setval('public.expenses_expense_id_seq', 1, false);
          public          postgres    false    221            7           0    0    log_entries_id_seq    SEQUENCE SET     @   SELECT pg_catalog.setval('public.log_entries_id_seq', 1, true);
          public          postgres    false    217                       2606    16406    cards cards_pkey 
   CONSTRAINT     S   ALTER TABLE ONLY public.cards
    ADD CONSTRAINT cards_pkey PRIMARY KEY (card_id);
 :   ALTER TABLE ONLY public.cards DROP CONSTRAINT cards_pkey;
       public            postgres    false    214            �           2606    24670    customers customers_email_key 
   CONSTRAINT     Y   ALTER TABLE ONLY public.customers
    ADD CONSTRAINT customers_email_key UNIQUE (email);
 G   ALTER TABLE ONLY public.customers DROP CONSTRAINT customers_email_key;
       public            postgres    false    215            �           2606    24676    customers customers_pkey 
   CONSTRAINT     Y   ALTER TABLE ONLY public.customers
    ADD CONSTRAINT customers_pkey PRIMARY KEY (email);
 B   ALTER TABLE ONLY public.customers DROP CONSTRAINT customers_pkey;
       public            postgres    false    215            �           2606    25983 7   expense_categories expense_categories_category_name_key 
   CONSTRAINT     {   ALTER TABLE ONLY public.expense_categories
    ADD CONSTRAINT expense_categories_category_name_key UNIQUE (category_name);
 a   ALTER TABLE ONLY public.expense_categories DROP CONSTRAINT expense_categories_category_name_key;
       public            postgres    false    220            �           2606    25961 (   expense_categories expense_category_pkey 
   CONSTRAINT     o   ALTER TABLE ONLY public.expense_categories
    ADD CONSTRAINT expense_category_pkey PRIMARY KEY (category_id);
 R   ALTER TABLE ONLY public.expense_categories DROP CONSTRAINT expense_category_pkey;
       public            postgres    false    220            �           2606    25969    expenses expenses_pkey 
   CONSTRAINT     \   ALTER TABLE ONLY public.expenses
    ADD CONSTRAINT expenses_pkey PRIMARY KEY (expense_id);
 @   ALTER TABLE ONLY public.expenses DROP CONSTRAINT expenses_pkey;
       public            postgres    false    222            �           2606    25906    log_entries log_entries_pkey 
   CONSTRAINT     Z   ALTER TABLE ONLY public.log_entries
    ADD CONSTRAINT log_entries_pkey PRIMARY KEY (id);
 F   ALTER TABLE ONLY public.log_entries DROP CONSTRAINT log_entries_pkey;
       public            postgres    false    218            �           2606    25891    users users_pkey 
   CONSTRAINT     U   ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pkey PRIMARY KEY (user_name);
 :   ALTER TABLE ONLY public.users DROP CONSTRAINT users_pkey;
       public            postgres    false    216            �           2606    25892    cards Cards_customer_email_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.cards
    ADD CONSTRAINT "Cards_customer_email_fkey" FOREIGN KEY (customer_email) REFERENCES public.customers(email) NOT VALID;
 K   ALTER TABLE ONLY public.cards DROP CONSTRAINT "Cards_customer_email_fkey";
       public          postgres    false    214    3201    215            �           2606    25970 "   expenses expenses_category_id_fkey    FK CONSTRAINT     �   ALTER TABLE ONLY public.expenses
    ADD CONSTRAINT expenses_category_id_fkey FOREIGN KEY (category_id) REFERENCES public.expense_categories(category_id) NOT VALID;
 L   ALTER TABLE ONLY public.expenses DROP CONSTRAINT expenses_category_id_fkey;
       public          postgres    false    222    3211    220               ,   x�2 3�?�@0���@0�LS��4A0A�=... �M            x������ � �      $   W   x��K� �u{
O`�'��n�h"<,���,t>�.�W
�І)�GV�F���KբR�Ӆ҆���
�$7d�	�#Im      &      x������ � �      "     x��VMo1=ï�H�Z�%(��,���X�D�����m���k�K!QQ�Rq������3��Z���T�#��՚hjʴfR����B�9 X�@h�(10���.]�����[��X�n/(̌3�x���<�q�D�"��@��]��%��8�B 5R�H�����Q�6�H1����ȫlX!��Uх��@D/��1$�/Ib&�]����JA^K�F�����,�a:��1�\����O��5��aaB�sn*v{ =�Z�*
���y��/' �D>��o��tJD���s�a%C/��<3��a������Yvޑ�5,.~��R�bg��r)pk���5�.�Jj92�-3�KE��"�$�
pϾ;�p�-�7��5K�M7����ٟ���A-��ҳ��!�_[̧�\^z��l�͕`��~��9�ߏ�pvN�YW>�{2��(�k6�9�X��AQ�MU�I�2y﮳r�F�M�c]�3���e�����@�����J:F:A��\Ї:�3'ǚ��gPl���)���co/���v|�sA.{� �*�;LK�]��\����/ߦ�0�ո/�� 9W�7�t���p6T�zHb�"2c8���E'52X�H�����
��A3��I� !:̉-���q0�ޒ�&g��H5���fv
��W�l�vބ+��'Q��9��ľ��;�tɸe%���	�9j=a��q�ϑP���R���h�j�[��V���ƾ�ط�V�o5J�����C�C�i��l����iݫ��\.�/�$          y   x��;�0D��0(���:w�n�CJA@���i����K)�$H�N��tjӇNnec4Q�S�U�J�Jj��6)�AF\�j6/^�x�Y�������v��<>u��:���-����%     