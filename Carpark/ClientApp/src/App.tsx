import React from 'react';
import './App.scss';
import bind from "bind-decorator";
import DatePicker from "react-datepicker";
import "react-datepicker/dist/react-datepicker.css";
import { Row, Col, Button, Container} from 'reactstrap';
import { calcRate } from './api';
import { Rate } from './model';

interface State {
  entryDate: Date;
  exitDate: Date;
  rateInfo?: Rate;
}

class App extends React.PureComponent<{}, State> {

  public readonly state: State = {
    entryDate: new Date(), exitDate: new Date()
  }

  public render() {
    return (
        <Container className="container">
          <h2>Rate calculator</h2>
          <Row>
            <Col>
              <h4>Car Entry Date</h4>
              <DatePicker showTimeSelect locale="en-AU" dateFormat="d/M/yyyy h:mm a" selected={this.state.entryDate} onChange={this.handleEntryChange} />
            </Col>

            <Col>
              <h4>Car Exit Date</h4>
              <DatePicker showTimeSelect locale="en-AU" dateFormat="d/M/yyyy h:mm a" selected={this.state.exitDate} onChange={this.handleExitChange} />
            </Col>

          </Row>

          <Button className="button" color="primary" onClick={this.handleOnClick}>Submit</Button>
          
         
          {this.state.rateInfo && <div className="rateInfo">
            <div>
              <h3>Rate: {this.state.rateInfo.name}</h3>
            </div>
            <div>
              <h3>Price: ${this.state.rateInfo.price}</h3>
            </div>
          </div>}

        </Container>     
    );
  }

  @bind
  private handleEntryChange(date: Date) {
    this.setState({
      entryDate: date
    });
  }

  @bind
  private handleExitChange(date: Date) {
    this.setState({
      exitDate: date
    });
  }

  @bind
  public async handleOnClick(event: any) {

    this.setState({
      rateInfo: (await calcRate(this.state.entryDate, this.state.exitDate))
    });
  }
}

export default App;