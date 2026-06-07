import { Routes, Route, NavLink } from "react-router-dom";
import Products from "./Products.jsx";
import Categories from "./Categories.jsx";

export default function App() {
  return (
    <div className="container">
      <h1>🍺 MyShop</h1>
      <nav className="nav">
        <NavLink to="/" end>Products</NavLink>
        <NavLink to="/categories">Categories</NavLink>
      </nav>
      <div className="card">
        <Routes>
          <Route path="/" element={<Products />} />
          <Route path="/categories" element={<Categories />} />
        </Routes>
      </div>
    </div>
  );
}