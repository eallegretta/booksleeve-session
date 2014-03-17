#Booksleeve.Session

A wrapper of [BookSleeve](https://code.google.com/p/booksleeve/) to easily use a persistent redis connection with connection string support

## Usage

Create a new RedisSesison instance and store it anywhere as a static memeber

    public static readonly Session = new RedisSession();

Just call the GetConnection method to access BookSeleeve magic

    Session.GetConnection()

Yuuuuuup, that's it

