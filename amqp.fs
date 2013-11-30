
\ bit			bit		[single bit]
\ class­id		short
\ consumer­tag		shortstr	[consumer tag] Identifier for the consumer, valid within the current channel.
\ delivery­tag		longlong	[server­assigned delivery tag] The server­assigned and channel­specific delivery tag
\ exchange­name		shortstr	[exchange name] The exchange name is a client­selected string that identifies the exchange for publish methods. 
\ long			long		[32­bit integer]
\ longlong		longlong	[64­bit integer]
\ longstr		longstr		[long string]
\ message­count		long		[number of messages in queue] The number of messages in the queue, which will be zero for newly­declared queues. This is the number of messages present in the queue, and committed if the channel on which they were published is transacted, that are not waiting acknowledgement.
\ method­id		short
\ no­ack		bit		[no acknowledgement needed] If this field is set the server does not expect acknowledgements for messages. That is, when a message is delivered to the client the server assumes the delivery will succeed and immediately dequeues it. This functionality may increase performance but at the cost of reliability. Messages can get lost if a client dies before they are delivered to the application.
\ no­local		bit		[do not deliver own messages] If the no­local field is set the server will not send messages to the connection that published them.
\ no­wait		bit		[do not send reply method] If set, the server will not respond to the method. The client should not wait for a reply method. If the server could not complete the method it will raise a channel or connection exception.
\ octet			octet		[single octet]
\ path			shortstr	Unconstrained.
\ peer­properties	table		This table provides a set of peer properties, used for identification, debugging, and general information.
\ queue­name		shortstr	[queue name] The queue name identifies the queue within the vhost. In methods where the queue name may be blank, and that has no specific significance, this refers to the 'current' queue for the channel, meaning the last queue that the client declared on the channel. If the client did not declare a queue, and the method needs a queue name, this will result in a 502 (syntax error) channel exception.
\ redelivered		bit		[message is being redelivered] This indicates that the message has been previously delivered to this or another client.
\ reply­code		short		[reply code from server] The reply code. The AMQ reply codes are defined as constants at the start of this formal specification.
\ reply­text		shortstr	[localised reply text] The localised reply text. This text can be logged as an aid to resolving issues.
\ short			short		[16­bit integer]
\ shortstr		shortstr	[short string]
\ table			table		[field table]
\ timestamp		timestamp	[64­bit timestamp]


1 constant frame­method
2 constant frame­header
3 constant frame-body
8 constant frame­heartbeat
4096 constant frame­min­size
206 constant frame­end
200 constant reply­success		\ Indicates that the method completed successfully. This reply code is reserved for future use ­ the current protocol design does not use positive confirmation and reply codes are sent only in case of an error.
311 constant content­too­large		\ [channel] The client attempted to transfer content larger than the server could accept at the present time. The client may retry at a later time.
313 constant no­consumers		\ [channel] When the exchange cannot deliver to a consumer when the immediate flag is set. As a result of pending data on the queue or the absence of any consumers of the queue.
320 constant connection­forced		\ [connection] An operator intervened to close the connection for some reason. The client may retry at some later date.
402 constant invalid­path		\ [connection] The client tried to work with an unknown virtual host.
403 constant access­refused		\ [channel] The client attempted to work with a server entity to which it has no access due to security settings.
404 constant not­found			\ [channel] The client attempted to work with a server entity that does not exist.
405 constant resource­locked		\ [channel] The client attempted to work with a server entity to which it has no access because another client is working with it.
406 constant precondition­failed	\ [channel] The client requested a method that was not allowed because some precondition failed.
501 constant frame­error		\ [connection] The sender sent a malformed frame that the recipient could not decode. This strongly implies a programming error in the sending peer.
502 constant syntax­error		\ [connection] The sender sent a frame that contained illegal values for one or more fields. This strongly implies a programming error in the sending peer.
503 constant command­invalid		\ [connection] The client sent an invalid sequence of frames, attempting to perform an operation that was considered invalid by the server. This usually implies a programming error in the client.
504 constant channel­error		\ [connection] The client attempted to work with a channel that had not been correctly opened. This most likely indicates a fault in the client layer.
505 constant unexpected­frame		\ [connection] The peer sent a frame that was not expected, usually in the context of a content header and body. This strongly indicates a fault in the peer's content processing.
506 constant resource­error		\ [connection] The server could not complete the method because it lacked sufficient resources. This may be due to the client creating too many of some type of entity.
530 constant not­allowed		\ [connection] The client tried to work with some entity in a manner that is prohibited by the server, due to security settings or by some other criteria.
540 constant not­implemented		\ [connection] The client tried to use functionality that is not implemented in the server.
541 constant internal-error		\ [connection] The server could not complete the method because of an internal error. The server may require intervention by an operator in order to resume normal operations.


\ access components of the below tables: class-id , method-id ,
: class-id ( addr -- n ) @ ;
: method-id ( addr -- n ) cell+ @ ;

\ connection
create connection.start 10 , 10 ,
create connection.start-ok 10 , 11 ,
create connection.secure 10 , 20 ,
create connection.secure-ok 10 , 21 ,
create connection.tune 10 , 30 ,
create connection.tune-ok 10 , 31 ,
create connection.open 10 , 40 ,
create connection.open-ok 10 , 41 ,
create connection.close 10 , 50 ,
create connection.close-ok 10 , 51 ,

\ channel
create channel.open 20 , 10 ,
create channel.open-ok 20 , 11 ,
create channel.flow 20 , 20 ,
create channel.flow-ok 20 , 21 ,
create channel.close 20 , 40 ,
create channel.close-ok 20 , 40 ,

\ exchange
create exchange.declare 40 , 10 ,
create exchange.declare-ok 40 , 11 ,
create exchange.delete 40 , 20 , 
create exchange.delete-ok 40 , 21 , 

\ queue
create queue.declare 50 , 10 ,
create queue.declare-ok 50 , 11 ,
create queue.bind 50 , 20 ,
create queue.bind-ok 50 , 21 ,
create queue.purge 50 , 30 ,
create queue.purge-ok 50 , 31 ,
create queue.delete 50 , 40 ,
create queue.delete-ok 50 , 41 ,
create queue.unbind 50 , 50 ,
create queue.unbind-ok 50 , 51 ,

\ basic
create basic.qos 60 , 10 ,
create basic.qos-ok 60 , 10 ,
create basic.consume 60 , 20 ,
create basic.consume-ok 60 , 21 ,
create basic.cancel 60 , 30 ,
create basic.cancel-ok 60 , 31 ,
create basic.publish 60 , 40 ,
create basic.return 60 , 50 ,
create basic.deliver 60 , 60 ,
create basic.get 60 , 70 ,
create basic.get-ok 60 , 71 ,
create basic.get-empty 60 , 72 ,
create basic.ack 60 , 80 ,
create basic.reject 60 , 90 , 
create basic.recover­async 60 , 100 ,
create basic.recover 60 , 110 ,
create basic.recover­ok 60 , 111 ,

\ tx
create tx.select 90 , 10 ,
create tx.select-ok 90 , 11 ,
create tx.commit 90 , 20 ,
create tx.commit-ok 90 , 21 ,
create tx.rollback 90 , 30 ,
create tx.rollback-ok 90 , 31 ,

: AMQP 65 c, 77 c, 81 c, 80 c,  ;
: protocol-id 	0 c, ;
: protocol-version  0 c, 9 c, 1 c,  ;
: protocol-header AMQP protocol-id protocol-version ;

: short-uint, ( u -- ) dup $ff00 and 8 rshift c, $ff and c, ;
: long-uint, ( u -- ) dup 16 rshift short-uint, short-uint, ;
: longlong-uint, ( u -- ) dup 32 rshift long-uint, long-uint, ;
: shortstr, ( caddr -- ) dup c@ 1+ dup allot here swap cmove ;
: longstr, ( addr u -- ) dup 4 + allot dup long-uint here swap cmove ; \ 32 bit length + u length string
: timestamp, ( u -- ) longlong-uint, ; 

: field-table, ( upairs -- ) long-uint, ; 	\ data to follow
: field-name, ( caddr -- ) shortstr,
: field-value-boolean, ( flag -- ) [char] t c, c, ;
: field-value-u8, ( byte -- ) [char] B c, c, ;
: field-value-s8, ( char -- ) [char] b c, c, ;
: field-value-s16, ( short -- ) [char] U c, short-uint, ;
: field-value-u16, ( short -- ) [char] u c, short-uint, ;
: field-value-s32, ( long -- ) [char] I c, long-uint, ;
: field-value-u32, ( long -- ) [char] i c, long-uint, ;
: field-value-s64, ( longlong -- ) [char] L c, longlong-uint, ; 
: field-value-u64, ( longlong -- ) [char] l c, longlong-uint, ; 
: field-value-float, ( F:float -- ) [char] f c, here 1 sfloats allot df! ;	\ 32 bit sfloats are rare in gforth
: field-value-double, ( F:double -- ) [char] d c, f, ;
: field-value-decimal, ( u scale -- ) [char] D c, c, long-uint, ;	\ don't quite grok this yet
: field-value-shortstr, ( caddr -- ) [char] s c, shortstr, ;
: field-value-longstr, ( addr u -- ) [char] S c, longstr, ;
: field-value-array, ( length -- ) [char] A c, ;				\ data to follow....
: field-value-timestamp, ( u -- ) [char] T c, longlong-uint, ;
: field-value-table, ( upairs -- ) [char] F c, field-table, ;
: field-value-none, ( -- ) [char] V c, ;

: frame-properties, ( length channel -- ) short-uint, long-uint, ;
: frame-end, $CE c, ;

\ method frames take a class-id, method-id and a list of fields
\ | class-id | method-id | arguments...
: method-payload, ( method-id class-id  ) short-uint, short-uint, ;	\ fields follows


\ class-id is the method frame class-id 
\ | class-id | weight | body size | property flags | property list...
: content-class, ( class -- ) c, ;
: content-weight, ( -- ) 0 c, ;
: content-body-size, ( u -- ) longlong-uint, ;
: property-flags, ( flags -- ) short-uint, ;		\ 16 bits of flags

\ header payloads
: header-payload, content-class, content-weight, content-body-size, property-flags, ; \ property-list follows 
: content-header, frame-properties, header-payload, frame-end, ;

\ content payloads
: content-body, 3 c, frame-properties, ; \ body-payload octets, frame-end, 
: content, 2 c, content-header, content-body, ;

: method-frame ( length channel -- ) 1 c, frame-properties, ;	\ method-payload frame-end ; 

\ heartbeats must be channel 0
: heartbeat 8 c, 0 c, 0 c, frame-end, ;



