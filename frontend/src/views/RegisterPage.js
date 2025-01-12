import React from "react";
import PreregisterNavbar from "components/Navbars/PreregisterNavbar";
import GoogleButton from "react-google-button";
import { useAuth } from "../context/AuthContext";

import {
  Badge,
  Button,
  Card,
  Form,
  Navbar,
  Nav,
  Container,
  Row,
  Col,
} from "react-bootstrap";

function User() {
  const mainPanel = React.useRef(null);
  const { login } = useAuth();

  return (
    <>
      <div className="wrapper">
        {/* <Sidebar color={color} image={hasImage ? image : ""} routes={routes} /> */}
        <div className="main-panel" ref={mainPanel}>
          <PreregisterNavbar />
          <div className="content">
            <Container fluid>
              <Row>
                <Col md="12">
                  <Card>
                    <Card.Header>
                      <Card.Title as="h4">Registrate</Card.Title>
                    </Card.Header>
                    <Card.Body>
                      <GoogleButton
                        label="Login con Google"
                        onClick={() => {
                          login();
                        }}
                      />
                    </Card.Body>
                  </Card>
                </Col>
              </Row>
            </Container>
          </div>
          {/* <Footer /> */}
        </div>
      </div>
    </>
  );
}

export default User;
