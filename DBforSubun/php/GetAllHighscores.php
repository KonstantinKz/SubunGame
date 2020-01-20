<?php

$sql = "SELECT * FROM highscores;";
$result = $conn->query($sql);

$arr = array();
if ($result->num_rows > 0) {   
    while($row = $result->fetch_assoc()) {
        $arr[] =$row;
    }

    echo json_encode($arr);
    
} else {
    echo "0 results";
    http_response_code(404);
}
http_response_code(200);
$conn->close();
?> 