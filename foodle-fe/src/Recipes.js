import React, { useEffect, useRef, useState } from "react";
import { Navigate, useNavigate } from "react-router-dom";
import { Button, Nav, Navbar, NavDropdown, Form, Container, Card } from "react-bootstrap";
import ProgressBar from "react-bootstrap/ProgressBar";
import DropdownButton from "react-bootstrap/DropdownButton";
import Dropdown from "react-bootstrap/Dropdown";
import axios from "axios";
import { MDBCard, MDBCardImage, MDBCardBody, MDBCardTitle, MDBCardFooter, MDBCardText, MDBRow, MDBCol, MDBBtn } from "mdb-react-ui-kit";

import "bootstrap/dist/css/bootstrap.min.css";
import "./styles/recipes.css";

export default function Recipes() {
	const navigate = useNavigate();

	const [categoryInputs, getCategoryData] = useState([]);
	const [categoryOutputs, setCategoryData] = useState([]);
	const [recipesInfo, getRecipesData] = useState([]);

	const categoryHandle = (e) => {
		setCategoryData(e);
	};
	useEffect(() => {
		// setLoading(false);
		// if(sessionStorage.getItem("token") === null) {
		//     navigate("/");
		// }
		fetchGetAllCategories();
		fetchGetAllRecipes();
	}, []);

	function formatDate(date) {
		const event = new Date(date);
		return event.toDateString();
	}

	const renderCard = (card) => {
		return (
			// <div className="card-margin">
			// 	<Card style={{ width: "18rem" }}>
			// 		<Card.Img variant="top" src={card.imageURL} className="card-img-top embed-responsive-item" />
			// 		<Card.Body className="card-img-top">
			// 			<Card.Title>{card.name}</Card.Title>
			// 			<Card.Text>{card.description}</Card.Text>
			// 			<Button variant="primary">Read more</Button>
			// 		</Card.Body>
			// 	</Card>
			// </div>
			<MDBCol>
				<MDBCard className="h-100">
					<MDBCardImage className="card-max-height" src={card.imageURL} alt="..." position="top" />
					<MDBCardBody>
						<MDBCardTitle>{card.name}</MDBCardTitle>
						<MDBCardText>{card.description}</MDBCardText>
					</MDBCardBody>
					{/* <button type="button" class="btn btn-primary">
						Read more
					</button> */}
					<MDBCardFooter className="text-muted">{formatDate(card.creationTime)}</MDBCardFooter>
				</MDBCard>
			</MDBCol>
		);
	};

	const renderCards = (card) => {
		return (
			<MDBCol>
				<MDBCard className="h-100">
					<MDBCardImage className="card-max-height" src={card.imageURL} alt="..." position="top" />
					<MDBCardBody>
						<MDBCardTitle>{card.name}</MDBCardTitle>
						<MDBCardText>{card.description}</MDBCardText>
					</MDBCardBody>
					<MDBCardFooter className="text-muted">{formatDate(card.creationTime)}</MDBCardFooter>
				</MDBCard>
			</MDBCol>
		);
	};

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

	function fetchGetAllCategories() {
		axios.get("http://localhost:5164/api/categories").then((res) => getCategoryData(res.data));
	}

	function fetchGetAllRecipes() {
		axios.get("http://localhost:5164/api/recipes").then((res) => getRecipesData(res.data));
	}

	function directToRegister(e) {
		navigate("/register");
	}

	function directToLogin(e) {
		navigate("/login");
	}

	return (
		<>
			{/* --- Navigation --- */}
			{/* <div className="space"></div>
			<Navbar collapseOnSelect expand="lg" bg="white" variant="white">
				<Container className="nav-container-background">
					<Navbar.Brand id="foodle-logo" href="#home">
						Foodle
					</Navbar.Brand>
					<Navbar.Toggle aria-controls="responsive-navbar-nav" />
					<Navbar.Collapse id="responsive-navbar-nav">
						<Nav className="me-auto">
							<Nav.Link href="#">Recipes</Nav.Link>
							<Nav.Link href="/createRecipe">Create recipe</Nav.Link>
							<Nav.Link href="#">Categories</Nav.Link>
						</Nav>
						<Nav>
							<Nav.Link className="username-color" href="#deets">
								username222
							</Nav.Link>
							<Nav.Link eventKey={2} href="/">
								Logout
							</Nav.Link>
						</Nav>
					</Navbar.Collapse>
				</Container>
			</Navbar> */}
			<div className="space"></div>
			{renderNav(localStorage.getItem("username"))}
			{/* --- Search Filter --- */}
			<div className="space"></div>
			<div className="hero-image">
				<div className="on-image">
					<div className="wrapper">
						<div className="wrapper-element">
							<label className="filter-label">Search for your favorite recipe...</label>
							<input type="search" className="form-control rounded" placeholder="Search" aria-label="Search" />
						</div>
						<div className="wrapper-element"></div>
						<div className="wrapper-element">
							<label className="filter-label">Category</label>
							<DropdownButton
								id="btn btn-success dropdown-toggle"
								placeholder="Category"
								title={categoryOutputs}
								onSelect={categoryHandle}>
								{categoryInputs.map((items) => {
									return <Dropdown.Item eventKey={items.name}>{items.name}</Dropdown.Item>;
								})}
							</DropdownButton>
						</div>
						<div className="wrapper-element">
							<label className="filter-label">
								<span>&#8203;</span>
							</label>
							<br></br>
							<button type="button" className="btn btn-outline-success">
								Search recipe
							</button>
						</div>
					</div>
				</div>
			</div>
			{/* --- Recipes --- */}
			<div className="recipes-container">
				<MDBRow className="row-cols-1 row-cols-md-3 g-5">
					{recipesInfo.map((items) => {
						return renderCard(items);
					})}
				</MDBRow>
			</div>
			<div className="recipesFooter">
				<p className="footer-text-recipes">
					<br></br>Foodle @ Copyright, 2022
				</p>
			</div>
		</>
	);
}
