//TODO refactoring !!!!
(() => {
    let searchDB = [];
    let topRated = [];

    fetch("Movies/GetAllMoviesJson", { method: "GET" })
        .then(e => e.json())
        .then(e => searchDB = Array.from(e).map(el => el.Title));

    fetch("Movies/GetTopRatedMoviesJson", { method: "GET" })
        .then(e => e.json())
        .then(e => topRated = Array.from(e).map(
            ({ Title, Poster }) => {
                return { Poster, Title };
            }));

    const input = document.getElementById("search");
    const suggestionContainer = document.getElementById("suggestions");

    input.addEventListener('click', () => {
        suggestionContainer.style["dispaly"] = "flex";
    });

    input.addEventListener("input", () => {
        const value = input.value;

        while (suggestionContainer.firstChild) {
            suggestionContainer.removeChild(suggestionContainer.firstChild);
        }


        if (value.length) {
            suggestionContainer.classList.toggle("suggestionsTop", false);
            suggestionContainer.classList.toggle("suggestions", true);

            const suggestions = searchDB.filter(el =>
                el.match(input.value)
            );

            if (suggestions.length === 1) {
                suggestionContainer.style["height"] = "120px";
            }
            else if (suggestions.length === 0) {
                suggestionContainer.style["height"] = "0px";
            }
            else {
                suggestionContainer.style["height"] = "240px";
            }

            for (let item of suggestions) {
                let suggestionElement = document.createElement("div");

                suggestionElement.addEventListener('click', () => {
                    input.value = item;
                    document.forms["movieForm"].submit();
                });

                fetch('SearchBar/SearchBarTile', {
                    method: "POST",
                    body: JSON.stringify({ title: item }),
                    headers: {
                        'Content-Type': 'application/json'
                    }
                }).then(data => data.text())
                    .then(data => suggestionElement.innerHTML = data);

                suggestionContainer.appendChild(suggestionElement);
            }
        }
        else {
            suggestionContainer.classList.toggle("suggestions", false);
            suggestionContainer.classList.toggle("suggestionsTop", true);
            suggestionContainer.style["height"] = "320px";

            for (let item of topRated) {
                let topRatedElement = document.createElement("img");
                topRatedElement.style["margin"] = "5px 5px 5px 5px";

                topRatedElement.src = item.Poster;
                topRatedElement.alt = "img";
                topRatedElement.width = 100;
                topRatedElement.height = 150;

                topRatedElement.addEventListener('click', () => {
                    input.value = item.Title;
                    document.forms["movieForm"].submit();
                });

                suggestionContainer.appendChild(topRatedElement);
            }
        }
    });
})();