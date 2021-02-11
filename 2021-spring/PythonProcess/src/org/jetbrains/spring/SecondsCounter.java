package org.jetbrains.spring;

import java.util.Timer;
import java.util.TimerTask;

public class SecondsCounter {

    private Timer timer;

    public void startCounting() {
        timer = new Timer();
        timer.scheduleAtFixedRate(new PrintSecondsTask(), 1000, 1000);
    }

    public void stopCounting() {
        if (timer == null) {
            return;
        }

        timer.cancel();
        timer.purge();
    }

    private static class PrintSecondsTask extends TimerTask {

        private int elapsedSeconds = 0;

        @Override
        public void run() {
            elapsedSeconds++;
            System.out.println("Elapsed time: " + elapsedSeconds + "s");
        }
    }
}
