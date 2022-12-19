import Main from "./Main";
import Login from "./Login";
import Register from "./Register";
import Recipes from "./Recipes";
import CreateRecipe from "./CreateRecipe";
import CreateCategory from "./CreateCategory";
import { Route, Routes } from "react-router-dom";

function App() {
	return (
		<>
			<Routes>
				<Route path="" element={<Main />} />
				<Route path="/login" element={<Login />} />
				<Route path="/register" element={<Register />} />
				<Route path="/recipes" element={<Recipes />} />
				<Route path="/createRecipe" element={<CreateRecipe />} />
				<Route path="/createCategory" element={<CreateCategory />} />
			</Routes>
		</>
	);
}

export default App;
