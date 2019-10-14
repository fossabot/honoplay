import React from 'react';
import CssBaseline from '@material-ui/core/CssBaseline';
import { Paper, Typography, IconButton, Divider } from '@material-ui/core';
import moment from 'moment';
import Style from './CardStyle';
import Edit from '@material-ui/icons/Edit';
import { Link } from 'react-router-dom';

export default function Card(props) {
  const { data, url } = props;
  const classes = Style();

  function handleOpen(data) {
    props.name(data.name);
    props.id(data.id);
  }
  return (
    <React.Fragment>
      <CssBaseline />
      <div className={classes.root}>
        <div className={classes.divRoot}>
          {data &&
            data.map((data, index) => {
              const dateToFormat = data.createdAt;
              return (
                <div className={classes.div1} key={index}>
                  <div className={classes.div}>
                    <div className={classes.div2}>
                      <Paper className={classes.paper}>
                        <div>
                          <div className={classes.box}>
                            <Typography color="primary" gutterBottom>
                              {data.name}
                            </Typography>
                            <Typography
                              variant="body2"
                              gutterBottom
                              className={classes.date}
                            >
                              {moment(dateToFormat).format('DD/MM/YYYY')}
                            </Typography>
                          </div>
                          <Divider />
                          <div className={classes.div4}>
                            <IconButton
                              component={Link}
                              to={
                                url
                                  ? `/trainingseries/${url}/${data.id}`
                                  : `/trainingseries/${data.id}`
                              }
                              onClick={() => handleOpen(data)}
                            >
                              <Edit />
                            </IconButton>
                          </div>
                        </div>
                      </Paper>
                    </div>
                  </div>
                </div>
              );
            })}
        </div>
      </div>
    </React.Fragment>
  );
}
