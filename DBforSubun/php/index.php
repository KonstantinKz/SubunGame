<?php
include 'connect.php';
require 'AltoRouter.php';

$router = new AltoRouter();

$router->map('GET', '/highscores','GetAllHighscores');
$router->map('POST', '/highscores','PostHighscore');

$match = $router->match();
if($match){
include $match["target"].".php";
}else{
    echo ":(";
    http_response_code(404);
}
?> 