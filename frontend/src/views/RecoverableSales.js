import React from "react";

// react-bootstrap components
import {
  Badge,
  Button,
  Card,
  Navbar,
  Nav,
  Table,
  Container,
  Row,
  Col,
} from "react-bootstrap";

function TableList() {
  return (
    <>
      <Container fluid>
        <Row>
          <Col md="12">
            <Card className="strpied-tabled-with-hover">
              <Card.Header>
                <Card.Title as="h4">Listado</Card.Title>
                <p className="card-category">
                  Consulta el estado de tus ventas recuperables.
                </p>
              </Card.Header>
              <Card.Body className="table-full-width table-responsive px-0">
                <Table className="table-hover table-striped">
                  <thead>
                    <tr>
                      <th className="border-0">ID</th>
                      <th className="border-0">Spotify Link</th>
                      <th className="border-0">Ingresos anuales</th>
                      <th className="border-0">Cantidad recuperación</th>
                      <th className="border-0">Fecha de comienzo</th>
                      <th className="border-0">Fecha de fin</th>
                    </tr>
                  </thead>
                  <tbody>
                    <tr>
                      <td>1</td>
                      <td>
                        <a href="spotify.com/profile/667731245">
                          spotify.com/profile/3424219321
                        </a>
                      </td>
                      <td>€ 5,100</td>
                      <td>€ 5,900</td>
                      <td>02/03/2024</td>
                      <td>02/03/2025</td>
                    </tr>
                    <tr>
                      <td>2</td>
                      <td>
                        <a href="spotify.com/profile/667731245">
                          spotify.com/profile/667731245
                        </a>
                      </td>
                      <td>€ 25,300</td>
                      <td>€ 29,700</td>
                      <td>24/06/2024</td>
                      <td>24/06/2025</td>
                    </tr>
                    <tr>
                      <td>3</td>
                      <td>
                        <a href="spotify.com/profile/3317731245">
                          spotify.com/profile/3317731245
                        </a>
                      </td>
                      <td>€ 8,102</td>
                      <td>€ 9,700</td>
                      <td>11/07/2024</td>
                      <td>11/07/2025</td>
                    </tr>
                  </tbody>
                </Table>
              </Card.Body>
            </Card>
          </Col>
          {/* <Col md="12">
            <Card className="card-plain table-plain-bg">
              <Card.Header>
                <Card.Title as="h4">Table on Plain Background</Card.Title>
                <p className="card-category">
                  Here is a subtitle for this table
                </p>
              </Card.Header>
              <Card.Body className="table-full-width table-responsive px-0">
                <Table className="table-hover">
                  <thead>
                    <tr>
                      <th className="border-0">ID</th>
                      <th className="border-0">Name</th>
                      <th className="border-0">Salary</th>
                      <th className="border-0">Country</th>
                      <th className="border-0">City</th>
                    </tr>
                  </thead>
                  <tbody>
                    <tr>
                      <td>1</td>
                      <td>Dakota Rice</td>
                      <td>$36,738</td>
                      <td>Niger</td>
                      <td>Oud-Turnhout</td>
                    </tr>
                    <tr>
                      <td>2</td>
                      <td>Minerva Hooper</td>
                      <td>$23,789</td>
                      <td>Curaçao</td>
                      <td>Sinaai-Waas</td>
                    </tr>
                    <tr>
                      <td>3</td>
                      <td>Sage Rodriguez</td>
                      <td>$56,142</td>
                      <td>Netherlands</td>
                      <td>Baileux</td>
                    </tr>
                    <tr>
                      <td>4</td>
                      <td>Philip Chaney</td>
                      <td>$38,735</td>
                      <td>Korea, South</td>
                      <td>Overland Park</td>
                    </tr>
                    <tr>
                      <td>5</td>
                      <td>Doris Greene</td>
                      <td>$63,542</td>
                      <td>Malawi</td>
                      <td>Feldkirchen in Kärnten</td>
                    </tr>
                    <tr>
                      <td>6</td>
                      <td>Mason Porter</td>
                      <td>$78,615</td>
                      <td>Chile</td>
                      <td>Gloucester</td>
                    </tr>
                  </tbody>
                </Table>
              </Card.Body>
            </Card>
          </Col> */}
        </Row>
      </Container>
    </>
  );
}

export default TableList;
