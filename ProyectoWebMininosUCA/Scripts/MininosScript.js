const caroussel = document.querySelector(".caroussel");
const arrowBtns = document.querySelectorAll(".wrapper i");
const firstCardWidth = caroussel.querySelector(".card").offsetWidth;
const carousselChildren = [...caroussel.children];

let isDragging = false, startX, startScrollLeft;

arrowBtns.forEach(btn => {
    btn.addEventListener("click", () => {
        caroussel.scrollLeft += btn.id === "left" ? -firstCardWidth : firstCardWidth;
    });
});

const dragStart = (e) => {
    isDragging = true;
    caroussel.classList.add("dragging");

    startX = e.pageX;
    startScrollLeft = caroussel.scrollLeft;
}

const dragging = (e) => {
    if (!isDragging) return;

    e.preventDefault();

    caroussel.scrollLeft = startScrollLeft - (e.pageX - startX);
}

const dragStop = () => {
    isDragging = false;
    caroussel.classList.remove("dragging");
}

caroussel.addEventListener("mousedown", dragStart);
caroussel.addEventListener("mousemove", dragging);
caroussel.addEventListener("mouseup", dragStop);





const caroussel2 = document.querySelector("#caroussel2");
const arrowBtns2 = document.querySelectorAll(".wrapper i");
const firstCardWidth2 = caroussel2.querySelector(".card").offsetWidth;
const carousselChildren2 = [...caroussel2.children];

let isDragging2 = false;
let startX2;
let startScrollLeft2;

arrowBtns2.forEach(btn => {
    btn.addEventListener("click", () => {
        caroussel2.scrollLeft += btn.id === "left2" ? -firstCardWidth2 : firstCardWidth2;
    });
});

const dragStart2 = (e) => {
    isDragging2 = true;
    caroussel2.classList.add("dragging");
    startX2 = e.pageX;
    startScrollLeft2 = caroussel2.scrollLeft;
};

const dragging2 = (e) => {
    if (!isDragging2) return;
    e.preventDefault();
    caroussel2.scrollLeft = startScrollLeft2 - (e.pageX - startX2);
};

const dragStop2 = () => {
    isDragging2 = false;
    caroussel2.classList.remove("dragging");
};

caroussel2.addEventListener("mousedown", dragStart2);
caroussel2.addEventListener("mousemove", dragging2);
caroussel2.addEventListener("mouseup", dragStop2);




const caroussel3 = document.querySelector("#caroussel3");
const arrowBtns3 = document.querySelectorAll(".wrapper i");
const firstCardWidth3 = caroussel3.querySelector(".card").offsetWidth;
const carousselChildren3 = [...caroussel3.children];

let isDragging3 = false;
let startX3;
let startScrollLeft3;

arrowBtns3.forEach(btn => {
    btn.addEventListener("click", () => {
        caroussel3.scrollLeft += btn.id === "left3" ? -firstCardWidth3 : firstCardWidth3;
    });
});

const dragStart3 = (e) => {
    isDragging3 = true;
    caroussel3.classList.add("dragging");
    startX3 = e.pageX;
    startScrollLeft3 = caroussel3.scrollLeft;
};

const dragging3 = (e) => {
    if (!isDragging3) return;
    e.preventDefault();
    caroussel3.scrollLeft = startScrollLeft3 - (e.pageX - startX3);
};

const dragStop3 = () => {
    isDragging3 = false;
    caroussel3.classList.remove("dragging");
};

caroussel3.addEventListener("mousedown", dragStart3);
caroussel3.addEventListener("mousemove", dragging3);
caroussel3.addEventListener("mouseup", dragStop3);

