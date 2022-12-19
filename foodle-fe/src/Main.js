import React, { useRef, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import "bootstrap/dist/css/bootstrap.min.css";
import "./styles/main.css";
import { Button } from "react-bootstrap";

export default function Main() {
	const navigate = useNavigate();

	useEffect(() => {
		sessionStorage.clear();
	});

	function directToRegister(e) {
		navigate("/register");
	}

	function directToLogin(e) {
		navigate("/login");
	}

	return (
		<>
			<section id="hero">
				<div className="container">
					<div className="info">
						<h1>Foodle</h1>
						<h2>Your smart cooking sidekick!</h2>
						<p>
							From recipe recommendations just for you to handy tools and helpful videos. Foodle has everything you need to improve life
							in the kitchen!
						</p>
						<div className="buttons">
							<div className="btn button-margin-right-30">
								<Button class="btn btn-success button-margin-right-30" variant="success" onClick={directToRegister}>
									Join now for free
								</Button>
							</div>
							<Button variant="outline-success" onClick={directToLogin}>
								Sign in
							</Button>
						</div>
					</div>
				</div>
				<footer>
					<p>Foodle @ Copyright, 2022</p>
				</footer>
			</section>
		</>
	);
}
