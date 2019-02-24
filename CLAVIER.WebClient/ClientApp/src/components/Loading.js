import React, {Component} from 'react';

export class Loading extends Component {
    render() {
        return (
            <div className="loading">
                <div className="loading-text">
                    <div className="loading-message">Loading... Please Wait.</div>
                </div>
            </div>
        );
    }
}
