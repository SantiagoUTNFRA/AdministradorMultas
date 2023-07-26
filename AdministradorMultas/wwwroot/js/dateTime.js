function displayCurrentDateTime() {
    const dateElement = document.getElementById("date");
    const timeElement = document.getElementById("time");

    function updateDateTime() {
        const currentDateTime = new Date();

        const day = currentDateTime.getDate();
        const month = currentDateTime.getMonth() + 1;
        const year = currentDateTime.getFullYear();
        const hours = currentDateTime.getHours();
        const minutes = currentDateTime.getMinutes();
        const seconds = currentDateTime.getSeconds();

        const formattedDate = `${day < 10 ? "0" + day : day}/${month < 10 ? "0" + month : month}/${year}`;
        const formattedTime = `${hours < 10 ? "0" + hours : hours}:${minutes < 10 ? "0" + minutes : minutes}:${seconds < 10 ? "0" + seconds : seconds}`;

        dateElement.textContent = formattedDate;
        timeElement.textContent = formattedTime;
    }

    // Actualizar la fecha y la hora cada segundo
    setInterval(updateDateTime, 1000);
}

// Llamar a la función para mostrar la fecha y la hora actual
displayCurrentDateTime();
