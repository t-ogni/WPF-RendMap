select *
from messages m 
left join users s on m.uid_sender=s.id;

select *
from messages m 
left join users s on m.uid_sender=s.id;

insert into messages ( uid_sender, uid_reciever, text) values 
( (select id from users where login='i'), (select id from users where login='yak'), 
        N'test' ),
( (select id from users where login='yak'), (select id from users where login='i'), 
        N'test2' );

        
insert into messages ( uid_sender, uid_reciever, text) values 
( (select id from users where login='adm'), (select id from users where login='i'), 
        N'testadm' ),
( (select id from users where login='adm'), (select id from users where login='i'), 
        N'test2adm' );


select distinct id from users u 
left join messages sen on u.id=sen.uid_sender 
left join messages rec on u.id=rec.uid_reciever 
where sen.uid_reciever='1' or rec.uid_sender='1';