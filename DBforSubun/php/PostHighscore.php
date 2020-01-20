<?php
	$body = file_get_contents('php://input');
	$body = json_decode($body);
	
	$name = $body->name;
	$score = $body->score;
	
	$sql = "INSERT INTO highscores(name, score) VALUES ('$name', '$score')";
	
	if($conn->query($sql)===TRUE)
	{
		echo "Success";
		http_response_code(200);
	}	
	else
	{
		echo "Error: ".$conn->error;
		http_response_code(400);
	}
?>