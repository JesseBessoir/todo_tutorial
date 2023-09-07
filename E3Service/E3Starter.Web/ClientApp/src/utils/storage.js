const isLocalStorageAvailable = () => {
  try {
    localStorage.setItem("test", "test");
    localStorage.removeItem("test");
    return true;
  } catch (e) {
    return false;
  }
};

// In-memory storage as a fallback
const inMemoryStorage = {};

// Function to get value from storage
export const getItem = (key) => {
  if (isLocalStorageAvailable()) {
    return localStorage.getItem(key);
  }
  return inMemoryStorage[key] || null;
};

// Function to set value to storage
export const setItem = (key, value) => {
  if (isLocalStorageAvailable()) {
    localStorage.setItem(key, value);
  } else {
    inMemoryStorage[key] = value;
  }
};
