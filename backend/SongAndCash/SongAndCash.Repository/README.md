# SongAndCash.Repository
How to set up locally the database with docker MySQL:
1. Install and run docker
2. Get the latest docker mysql image: `docker pull mysql`
3. Get the latest docker phpadmin image: `docker pull phpmyadmin/phpmyadmin`
4. Create a new network for the database:
`docker network create songandcash_net`
5. Run the following command to create the database:
`docker run -d --network songandcash_net --name songandcash_db -p 3306:3306 -e MYSQL_ROOT_PASSWORD=ABCabc123. -d mysql:latest`
6. Run the following command to create the phpmyadmin:
`docker run -d --network songandcash_net --name songandcash_phpmyadmin -e PMA_HOST=songandcash_db -e PMA_PORT=3306 -p 8080:80 phpmyadmin/phpmyadmin`
7. Navigate to localhost:8080 in your browser