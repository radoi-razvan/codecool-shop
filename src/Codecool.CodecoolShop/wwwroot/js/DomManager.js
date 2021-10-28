export const domManager = {
    addChild(parentIdentifier, childContent) {
        let parent = document.querySelector(parentIdentifier);
        if (parent) {
            parent.insertAdjacentHTML("beforeend", childContent);
        } else {
            console.error("could not find such html element: " + parentIdentifier)
        }
    },

    addEventListener(parentIdentifier, eventType, eventHandler) {
        let parent = document.querySelector(parentIdentifier);
        if (parent) {
            parent.addEventListener(eventType, eventHandler);
        } else {
            console.error("could not find such html element: " + parentIdentifier)
        }
    },

    removeChildren(parentIdentifier) {
        let parent = document.querySelector(parentIdentifier);
        parent.innerHTML = '';
    }
};