import React, { useEffect, useRef, useState } from "react";
import { Navigate, useNavigate } from "react-router-dom";
import { Button, Nav, Navbar, NavDropdown, Form, Container, Card, Modal } from "react-bootstrap";
import ProgressBar from "react-bootstrap/ProgressBar";
import DropdownButton from "react-bootstrap/DropdownButton";
import Dropdown from "react-bootstrap/Dropdown";
import axios from "axios";
import { MDBCard, MDBCardImage, MDBCardBody, MDBCardTitle, MDBCardText, MDBRow, MDBCol, MDBBtn } from "mdb-react-ui-kit";

import "bootstrap/dist/css/bootstrap.min.css";
import "./styles/recipes.css";

export default function Recipes() {
	const navigate = useNavigate();

	const [show, setShow] = useState(false);

	const handleClose = (e) => {
		e.preventDefault(e);
		setShow(false);
	};
	const handleShow = (e) => {
		e.preventDefault(e);
		setShow(true);
	};

	const [categoryInputs, getCategoryData] = useState([]);
	const [categoryOutputs, setCategoryData] = useState([]);

	const [recipeName, setRecipeName] = useState([]);
	const [recipeImageUrl, setRecipeImageUrl] = useState([]);
	const [recipeDescription, setRecipeDescription] = useState([]);
	const [recipeCategoryId, setRecipeCategoryId] = useState([]);

	const categoryHandle = (e) => {
		setRecipeCategoryId(e);
		setCategoryData(e);
	};
	useEffect(() => {
		// setLoading(false);
		// if(sessionStorage.getItem("token") === null) {
		//     navigate("/");
		// }
		fetchGetAllCategories();
	}, []);

	function create() {
		setShow(false);
		if (recipeName.length === 0 || recipeDescription.length === 0) {
			let error = document.getElementById("errorCreate");
			error.textContent = "All fields should be filled!";
		} else {
			let json = {};
			json["Name"] = recipeName;
			json["Description"] = recipeDescription;

			if (JSON.stringify(json) != "{}") {
				json = JSON.stringify(json);
				fetchCreate(json);
			}
			navigate("/recipes");
		}
	}

	function fetchCreate(json) {
		const requestOptions = {
			method: "POST",
			headers: {
				Authorization: "Bearer " + localStorage.getItem("token"),
				"Content-Type": "application/json",
			},
			body: json,
		};
		fetch("http://localhost:5164/api/categories", requestOptions).then((res) => afterFetchCreate(res));
	}

	function afterFetchCreate(res) {
		if (res.status != 200) {
			let error = document.getElementById("errorCreate");
			res.text().then((result) => (error.textContent = result));
		} else {
			window.location.reload(true);
		}
	}

	function fetchGetAllCategories() {
		axios.get("http://localhost:5164/api/categories").then((res) => getCategoryData(res.data));
	}

	function directToLogin(e) {
		navigate("/login");
	}

	const renderNav = (isAdmin) => {
		if (isAdmin === "admin") {
			return (
				<Navbar collapseOnSelect expand="lg" bg="white" variant="white">
					<Container className="nav-container-background">
						<Navbar.Brand id="foodle-logo" href="#home">
							Foodle
						</Navbar.Brand>
						<Navbar.Toggle aria-controls="responsive-navbar-nav" />
						<Navbar.Collapse id="responsive-navbar-nav">
							<Nav className="me-auto">
								<Nav.Link href="/recipes">Recipes</Nav.Link>
								<Nav.Link href="/createRecipe">Create recipe</Nav.Link>
								<Nav.Link href="/createCategory">Create category</Nav.Link>
							</Nav>
							<Nav>
								<Nav.Link href="#">
									<div className="username-text-bold">{isAdmin}</div>
								</Nav.Link>
								<Nav.Link eventKey={2} href="/">
									Logout
								</Nav.Link>
							</Nav>
						</Navbar.Collapse>
					</Container>
				</Navbar>
			);
		} else {
			return (
				<Navbar collapseOnSelect expand="lg" bg="white" variant="white">
					<Container className="nav-container-background">
						<Navbar.Brand id="foodle-logo">Foodle</Navbar.Brand>
						<Navbar.Toggle aria-controls="responsive-navbar-nav" />
						<Navbar.Collapse id="responsive-navbar-nav">
							<Nav className="me-auto">
								<Nav.Link href="/recipes">Recipes</Nav.Link>
							</Nav>
							<Nav>
								<Nav.Link>Hi,</Nav.Link>
								<Nav.Link href="#">
									<div className="username-text-bold">{isAdmin}</div>
								</Nav.Link>
								<Nav.Link eventKey={2} href="/">
									Logout
								</Nav.Link>
							</Nav>
						</Navbar.Collapse>
					</Container>
				</Navbar>
			);
		}
	};

	return (
		<>
			{/* --- Navigation --- */}
			<div className="space"></div>
			{renderNav(localStorage.getItem("username"))}

			{/* --- Search Filter --- */}
			<div className="space"></div>
			<div className="hero-image">
				<p className="hero-text">Create new category</p>
			</div>
			{/* --- Recipes --- */}
			<div className="recipes-container">
				<div className="center-form">
					<form>
						<p id="errorCreate" className="errorTextTitle"></p>
						<br></br>
						<div className="form-outline mb-4">
							<label className="form-label" htmlFor="form4Example1">
								Category name
							</label>
							<input
								type="text"
								id="form4Example1"
								className="form-control"
								value={recipeName}
								onChange={(e) => {
									setRecipeName(e.target.value);
								}}
							/>
						</div>

						<div className="form-outline mb-4">
							<label className="form-label" htmlFor="form4Example3">
								Description
							</label>
							<textarea
								className="form-control"
								id="form4Example3"
								rows="4"
								value={recipeDescription}
								onChange={(e) => {
									setRecipeDescription(e.target.value);
								}}></textarea>
						</div>

						<button type="submit" className="btn btn-primary btn-block mb-4 button-margin-right-30" onClick={handleShow}>
							Create category
						</button>
					</form>
				</div>
			</div>
			<p>&ensp;</p>
			<p>&ensp;</p>
			<p>&ensp;</p>

			<div className="recipesFooter">
				<p className="footer-text-recipes">
					<br></br>Foodle @ Copyright, 2022
				</p>
			</div>
			<Modal show={show} onHide={handleClose}>
				<Modal.Header closeButton>
					<Modal.Title>New category</Modal.Title>
				</Modal.Header>
				<Modal.Body>Are you sure you want to create a new category?</Modal.Body>
				<Modal.Footer>
					<Button variant="primary" onClick={() => create()}>
						Create
					</Button>
					<Button variant="outline-primary" onClick={handleClose}>
						Close
					</Button>
				</Modal.Footer>
			</Modal>
		</>
	);
}
