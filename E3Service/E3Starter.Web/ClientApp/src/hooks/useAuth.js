import { create } from "zustand";
import { getItem, setItem } from "../utils/storage";

const useAuth = create((set) => ({
  token: getItem("token"),
  currentUser: JSON.parse(getItem("currentUser")),
  setToken: (token) => {
    setItem("token", token);
    set({ token });
  },
  setCurrentUser: (user) => {
    setItem("currentUser", JSON.stringify(user));
    set({ currentUser: user });
  },
  clearData: () => {
    setItem("token", null);
    setItem("currentUser", null);
    set({ token: null, currentUser: null });
  },
}));

export default useAuth;