import React, { useRef, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { Button } from "react-bootstrap";
import axios from "axios";
import jwt_decode from "jwt-decode";

export default function Login() {
	const navigate = useNavigate();
	const [username, setUsername] = useState([]);
	const [password, setPassword] = useState([]);
	const [msg, setMsg] = useState([]);

	const Auth = async (e) => {
		e.preventDefault();
		let errorMsg = "";
		if (username.length !== 0 && password.length !== 0) {
			try {
				let res = await axios.post("http://localhost:5164/api/login", {
					userName: username,
					password: password,
				});
				let dataToken = res.data.accessToken;
				localStorage.setItem("token", dataToken);
				localStorage.setItem("username", username);
				const decoded = jwt_decode(dataToken);
				localStorage.setItem("tokenExp", decoded.exp);
				navigate("/recipes");
			} catch (error) {
				if (error.response) {
					let error = document.getElementById("errorLogin");
					error.style.color = "red";
					error.textContent = "Incorrect username or password";
				}
			}
		}
	};

	useEffect(() => {
		sessionStorage.clear();
	});

	function directToRegister(e) {
		navigate("/register");
	}

	function directToMainPage(e) {
		navigate("/");
	}

	return (
		<>
			<div className="registerBody">
				<form className="form-signup center-middle" onSubmit={Auth}>
					<h2>Log in</h2>
					<p>Welcome back, please enter your login credentials.</p>
					<br></br>
					<p id="errorLogin"></p>
					<div className="form-group">
						<input
							type="text"
							className="form-control"
							value={username}
							onChange={(e) => setUsername(e.target.value)}
							name="username"
							placeholder="Username"
							required></input>
					</div>
					<br></br>
					<div className="form-group">
						<input
							type="password"
							className="form-control"
							value={password}
							onChange={(e) => setPassword(e.target.value)}
							name="password"
							placeholder="Password"
							required></input>
					</div>
					<br></br>
					<div className="row">
						<div class="col-md-6">
							<input type="submit" class="btn btn-success btn-block submit-button" name="" value="Submit"></input>
						</div>
						<div class="col-md-6">
							<Button variant="outline-success btn btn-block submit-button" onClick={directToMainPage}>
								Cancel
							</Button>
						</div>
					</div>
					<br></br>
					<p>
						Don't have an account yet?
						<Button variant="link" onClick={directToRegister}>
							Sign up
						</Button>
					</p>
				</form>
				<footer>
					<p>Foodle @ Copyright, 2022</p>
				</footer>
			</div>
		</>
	);
}
